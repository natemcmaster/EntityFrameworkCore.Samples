using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DockerWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:5000") // important! Kestrel can't bind to "localhost" inside a docker container
                .Build();

            host.Run();
        }
    }
}
