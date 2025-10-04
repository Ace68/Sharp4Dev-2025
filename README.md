# Sharp4Dev-2025

Professional .NET solution for managing the preparation of Cacio e Pepe.

## Project Structure

- `src/` - Main source code
  - `CacioPepe.Infrastructure/` - Infrastructure layer
  - `CacioPepe.Rest/` - REST API project
  - `CacioPepe.Shared/` - Shared constants and utilities
  - `Cucina/` - Kitchen domain modules
    - `CacioPepe.Cucina.Domain/` - Domain logic for kitchen
    - `CacioPepe.Cucina.Facade/` - Facade for kitchen operations
    - `CacioPepe.Cucina.Infrastructure/` - Infrastructure for kitchen
    - `CacioPepe.Cucina.ReadModel/` - Read models for kitchen
    - `CacioPepe.Cucina.SharedKernel/` - Shared kernel for kitchen
    - `CacioPepe.Cucina.Tests/` - Tests for kitchen domain
  - `Trattoria/` - Trattoria domain modules
    - `CacioPepe.Trattoria.Domain/` - Domain logic for trattoria
    - `CacioPepe.Trattoria.Facade/` - Facade for trattoria operations
    - `CacioPepe.Trattoria.Infrastructure/` - Infrastructure for trattoria
    - `CacioPepe.Trattoria.ReadModel/` - Read models for trattoria
    - `CacioPepe.Trattoria.SharedKernel/` - Shared kernel for trattoria
    - `CacioPepe.Trattoria.Tests/` - Tests for trattoria domain

## Getting Started

1. Clone the repository:
   ```sh
   git clone https://github.com/Ace68/Sharp4Dev-2025.git
   ```
2. Open the solution file (`CacioPepe.sln`) in Visual Studio or JetBrains Rider.
3. Restore NuGet packages and build the solution.
4. Run the `CacioPepe.Rest` project to start the API.

## Technologies Used

- .NET 6/7/8 (depending on project configuration)
- ASP.NET Core
- Domain-Driven Design (DDD)
- Modular architecture

## Conventions

- Code and comments are in English.
- Modular and reusable code structure.
- Professional coding standards.

## License

See `LICENSE` for details.

---

For more information, refer to the documentation in each module or contact the repository owner.