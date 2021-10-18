# basic-cms
Basic CMS project with clean architecture based on [this repo](https://github.com/jasontaylordev/CleanArchitecture)

## How To Run?

### Docker
Run `docker-compose up --build`

### Dotnet CLI
Add mssql server connection string to appsettings.json

```json
{
  "MainDb": {
    "ConnectionString": "Your_Connection_String"
  }
}
```

Run `dotnet run src/WebApi/WebApi.csproj`
