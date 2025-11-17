//// ---------------------------------------------
////  EF Core / Build / Deploy / Runtime Commands
//// ---------------------------------------------
//// dotnet ef database update  --project NIBAUTH.Persistence/NIBAUTH.Persistence.csproj   --startup-project NIBAUTH.WebApi/NIBAUTH.WebApi.csproj

//// dotnet ef migrations add initial --project NIBAUTH.Persistence/NIBAUTH.Persistence.csproj   --startup-project NIBAUTH.WebApi/NIBAUTH.WebApi.csproj

//// Shift+Option+F - Reformat (MacOS)
//// Ctrl+Shift+I    - Reformat (Windows)
//// dotnet publish NIBAUTH.WebApi/NIBAUTH.WebApi.csproj

//// Run CLI commands:
//// dotnet run --project NIBAUTH.WebApi/NIBAUTH.WebApi.csproj seed
//// dotnet run --project NIBAUTH.WebApi/NIBAUTH.WebApi.csproj temp


//// Docker Compose:
//// docker compose -f NIBAUTH.Deploy/docker-compose.publish.yml up -d
//// docker compose -f NIBAUTH.Deploy/docker-compose.publish.yml down

