Docker, ASP.NET Core, and PostgreSQL
------------------------------------

To run this sample, execute these commands.

```
dotnet publish --configuration Release DockerWebApp/
docker-compose up
```

Docker Compose will create two containers, one running the ASP.NET Core application,
and the other running a PostgreSQL database server.