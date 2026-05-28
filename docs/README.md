# PastasAPI

API REST para delivery de pastas artesanales desarrollada con .NET 8 y Clean Architecture.

## Tecnologías
- .NET 8
- Entity Framework Core
- SQLite
- JWT Authentication
- Swagger

## Cómo ejecutar
1. Clonar el repositorio
2. Ejecutar `dotnet restore`
3. Ejecutar `dotnet ef database update --context ApplicationContext --startup-project src/Web --project src/Infrastructure`
4. Ejecutar `dotnet run --project src/Web`
5. Abrir http://localhost:5043/swagger