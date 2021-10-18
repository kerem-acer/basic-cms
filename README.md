# .Net Core Basic CMS Project
Basic CMS project based on [this repo](https://github.com/jasontaylordev/CleanArchitecture)

## How To Run?
First clone repository.

Run `git clone https://github.com/kerem-acer/basic-cms.git`

then run `cd basic-cms`


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

Run `dotnet run --project src/WebApi/WebApi.csproj`
