For DB (while in .sln folder):
1. docker-compose up
2. docker-compose start

For Dependencies (migrations are in GSW-Data and startup project is GSW):
dotnet restore

For Migrations (while in .sln folder):
dotnet ef database update --project GSW-Data --startup-project GSW

Start the project:
dotnet run

Dependencies:
1. Docker
2. FluentValidation
3. EF Core
4. Swagger
