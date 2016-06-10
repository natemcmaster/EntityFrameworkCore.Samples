using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoggingSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = CreateOptions<LoggingDbContext>();

            using (var context = new LoggingDbContext(options))
            {
                if (context.Database.EnsureCreated())
                {
                    context.AddRange(SampleData.GetShips());
                    context.SaveChanges();
                }

                var allFrigates = (from s in context.Ships
                                   where s.VesselClass == "Frigate"
                                   select s)
                                  .Count();

                // how to get access to the logger factory EF is configured to use
                var logger = context
                        .GetInfrastructure()
                        .GetService<ILoggerFactory>()
                        .CreateLogger<Program>();

                logger.LogInformation("There are {0} frigates in the database", allFrigates);
            }
        }

        private static ILoggerFactory CreateLoggerFactory()
        {
            var loggerFactory = new LoggerFactory();

            // Writes log output to the VS "Debug" stream in the Output window
            loggerFactory.AddDebug();

            // Log to stdout
            loggerFactory.AddConsole(LogLevel.Information);

            // TODO use a "real" file logger. See https://github.com/aspnet/Logging/issues/441
            // Example of how to capture granular logging
            loggerFactory.AddProvider(new FileLoggerProvider("./query_plans.log",
                (category, e) =>
                    category.EndsWith(nameof(RelationalQueryCompilationContextFactory))
                    && e.Id == (int)Microsoft.EntityFrameworkCore.Infrastructure.CoreEventId.QueryPlan));

            return loggerFactory;
        }


        private static DbContextOptions<TContext> CreateOptions<TContext>()
            where TContext : DbContext
        {
            var loggerFactory = CreateLoggerFactory();

            var builder = new DbContextOptionsBuilder<TContext>();
            builder
                .UseSqlite("Filename=./test.db")
                .UseLoggerFactory(loggerFactory)
                // Log messages may contain sensitive info such as parameters and/or connection string data
                .EnableSensitiveDataLogging();
            return builder.Options;
        }
    }
}