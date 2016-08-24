ASP.NET Core Basic Sample
-------------------------

Demonstrates minimal setup of EF Core within an ASP.NET Core app.

[Install .NET Core CLI](https://dot.net) and run the following commands to build and launch the sample.

```
dotnet restore

cd WebApplication

dotnet build

dotnet ef database update

dotnet run
```