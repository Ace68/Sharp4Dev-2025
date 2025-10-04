# CacioPepe API

Una REST API .NET 9 per la gestione della preparazione dello spiedo bresciano, costruita seguendo i principi del Domain-Driven Design con architettura modulare.

## Architettura

La solution è organizzata seguendo una struttura modulare con separazione dei bounded context:

- **90 Presentation**: API REST entry point
- **80 Infrastructure**: Servizi di infrastruttura condivisi
- **50 Modules**: Moduli di dominio isolati
  - **Cucina**: Gestione della cucina
  - **Trattoria**: Gestione della trattoria
- **30 Shared**: Tipi e interfacce condivise

### Moduli

Ogni modulo segue la struttura:
- **Facade**: Esposizione endpoint e interfacce pubbliche
- **Domain**: Logiche e entità di dominio
- **Infrastructure**: Implementazioni di persistenza e servizi
- **ReadModel**: Modelli di lettura e query
- **SharedKernel**: Tipi condivisi del modulo
- **Tests**: Test architetturali con NetArchTest

## Getting Started

### Prerequisites

- .NET 9 SDK
- Visual Studio 2022 o VS Code

### Running the Application

1. Clonare il repository
2. Navigare nella cartella `src`
3. Eseguire l'applicazione:

```bash
cd CacioPepe.Rest
dotnet run
```

L'applicazione sarà disponibile su `http://localhost:5203`

### API Endpoints

- **GET /v1/cucina** - Status del modulo Cucina
- **GET /v1/trattoria** - Status del modulo Trattoria

### API Documentation

- **OpenAPI JSON**: `http://localhost:5203/openapi/v1.json`
- **Scalar UI**: `http://localhost:5203/scalar/v1`

## Build e Test

### Build della Solution

```bash
dotnet build CacioPepe.sln
```

### Esecuzione dei Test

```bash
dotnet test CacioPepe.sln
```

I test architetturali verificano:
- Isolamento tra moduli (nessuna dipendenza cross-module)
- Corrette convenzioni di naming dei namespace

### Build Configuration

La solution è configurata per:
- Compilazione senza warning critici
- Target Framework: .NET 9
- Nullable reference types abilitati

## Features Implementate

### OpenAPI & Documentation

- Documentazione OpenAPI v3 generata automaticamente
- UI Scalar per l'esplorazione delle API
- Versioning tramite path (`/v1/`)

### Telemetria (Configurabile)

- OpenTelemetry configurato ma disabilitato per default
- Supporto per Azure Monitor e OTLP
- Metriche automatiche per ASP.NET Core

### Logging

- Serilog configurato con output su Console e File
- Log strutturati in formato JSON
- Configurazione tramite appsettings.json

## Architettura e Principi

### Domain-Driven Design

- Bounded Context isolati (Cucina, Trattoria)
- Nessuna dipendenza tra moduli
- Facade pattern per l'esposizione dei servizi

### Test Architetturali

I test con NetArchTest garantiscono:
- Isolamento dei moduli
- Rispetto delle convenzioni di naming
- Dipendenze corrette tra layer

### Modular Monolith

- Struttura modulare con possibilità di estrazione futura in microservizi
- Interface-based module registration
- Dependency injection configurato per modulo

## Struttura File System

```
src/
├── CacioPepe.sln
├── CacioPepe.Rest/          # API REST entry point
├── CacioPepe.Shared/        # Tipi condivisi
├── CacioPepe.Infrastructure/ # Infrastruttura condivisa
├── Cucina/                  # Modulo Cucina
│   ├── CacioPepe.Cucina.Facade/
│   ├── CacioPepe.Cucina.Domain/
│   ├── CacioPepe.Cucina.Infrastructure/
│   ├── CacioPepe.Cucina.ReadModel/
│   ├── CacioPepe.Cucina.SharedKernel/
│   └── CacioPepe.Cucina.Tests/
└── Trattoria/               # Modulo Trattoria
    ├── CacioPepe.Trattoria.Facade/
    ├── CacioPepe.Trattoria.Domain/
    ├── CacioPepe.Trattoria.Infrastructure/
    ├── CacioPepe.Trattoria.ReadModel/
    ├── CacioPepe.Trattoria.SharedKernel/
    └── CacioPepe.Trattoria.Tests/
```

## Next Steps

- Implementazione di entità e logiche di dominio specifiche
- Configurazione di persistenza (Entity Framework Core)
- Implementazione di autenticazione e autorizzazione Azure AD
- Setup CI/CD per deployment su Azure Container Apps
- Configurazione completa di OpenTelemetry per monitoraggio

## Contributing

La solution segue le convenzioni:
- Codice e commenti in inglese
- Namespace: `CacioPepe.[Modulo].[Layer]`
- Test architetturali obbligatori per nuovi moduli
- Nessuna dipendenza cross-module permessa