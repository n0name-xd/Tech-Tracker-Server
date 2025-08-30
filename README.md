## bd
docker-compose up -d

## migrations
dotnet ef migrations add InitialCreate_v_0.01
dotnet ef database update
dotnet ef migrations remove
dotnet ef migrations add {tableName}
dotnet ef database drop