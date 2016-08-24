Logging
-------

The internal components of EF do an extensive amount of logging.

If you want to capture log output, configure EF to use an instance of `Microsoft.Extensions.Logging.ILoggerFactory`.

```c#
var myLogger = new LoggingFactory();

var optionsBuilder = new DbContextOptionsBuilder();
optionsBuilder
     .UseLoggingFactory(myLogger)
     .UseSqlServer(connString);

var db = new MyDbContext(optionsBuilder.Options);
```

This sample project, shows you various ways to use logging with EF.