# Prompt Migliorato / Linee Guida Architetturali SpiedoBresciano

Questo file integra e rafforza il prompt originale per garantire chiarezza, ripetibilità e qualità dell'implementazione.

---
## 1. Scopo e Criteri di Done
Obiettivo: fornire una soluzione modulare Minimal API (.NET 9) per i bounded context "Macelleria" e "Trattoria" basata sul template indicato.

La feature iniziale è considerata DONE quando:
- La solution compila (Debug/Release) senza warning critici (idealmente treat warnings as errors).
- Tutti i test architetturali (NetArchTest) passano.
- Endpoint di base esposti: `/v1/macelleria`, `/v1/trattoria` (anche vuoti) → HTTP 200/204.
- Swagger/OpenAPI accessibile (JSON + UI Scalar) e versione valorizzata.
- Telemetria configurabile (OpenTelemetry module disattivato finché non configurato).
- README aggiornato con istruzioni run locali.
- Nessuna dipendenza cross‑modulo non autorizzata (vedi matrice sotto).

---
## 2. Struttura Solution (Logica vs Fisica)
Solution folders (logici):
- 90 Presentation → `SpiedoBresciano.Rest`
- 80 Infrastructure → `SpiedoBresciano.Infrastructure`
- 50 Modules → sotto-folder per ciascun bounded context:
  - Macelleria: Facade, Domain, SharedKernel, ReadModel, Infrastructure, Tests
  - Trattoria: Facade, Domain, SharedKernel, ReadModel, Infrastructure, Tests
- 30 Shared → `SpiedoBresciano.Shared`

Cartelle fisiche richieste solo per `Macelleria/` e `Trattoria/` (contenenti i rispettivi progetti). Gli altri progetti restano in `src/`.

---
## 3. Matrice Dipendenze Consentite
| Da → A | Shared | Infrastructure (root) | {Modulo}.SharedKernel | {Modulo}.Domain | {Modulo}.ReadModel | {Modulo}.Infrastructure | {Modulo}.Facade | Altro Modulo.* | Rest |
|--------|--------|-----------------------|-----------------------|-----------------|--------------------|------------------------|-----------------|-----------------|------|
| Shared |  X     | (limita)              | NO                    | NO              | NO                 | NO                     | NO              | NO              | NO   |
| Infrastructure (root) | Shared | X | NO | NO | NO | NO | NO | NO | NO |
| {Modulo}.SharedKernel | Shared | NO | X | (opzionale Domain) | (opzionale) | NO | (solo se tipi base) | NO | NO |
| {Modulo}.Domain | {Modulo}.SharedKernel, Shared | NO | YES | X | (query DTO opzionale) | NO | NO (idealmente) | NO | NO |
| {Modulo}.ReadModel | {Modulo}.SharedKernel, Shared | (solo provider DB generico) | YES | NO (niente dominio → mantenere CQRS separation) | X | (eccezione se necessario per EF) | NO | NO | NO |
| {Modulo}.Infrastructure | {Modulo}.Domain, {Modulo}.SharedKernel, Shared | (può usare root Infrastructure) | YES | YES | YES | X | NO | NO | NO |
| {Modulo}.Facade | {Modulo}.Domain, {Modulo}.ReadModel, {Modulo}.SharedKernel, Shared | NO | YES | YES | YES | (limita) | X | NO | NO (usa DI in Rest) |
| Rest | Facade (Macelleria, Trattoria) + Shared | NO | NO | NO | NO | NO | YES (solo via Facade) | NO | X |

Regola sintetica: Rest conosce solo le Facade; cross‑modulo consentito solo tramite chiamate HTTP future o orchestrazioni esterne, non tramite riferimenti diretti.

---
## 4. Naming & Convenzioni
- Namespace radice: `SpiedoBresciano.[Modulo].[Strato]`.
- Endpoint group path: `/v1/{modulo}` (versione nel path per semplicità iniziale).
- File Module: `{NomeModulo}Module.cs` implementa `IModule`.
- Interfacce Facade: `IMacelleriaFacade`, `ITrattoriaFacade` (anche vuote inizialmente) in progetto Facade.
- Aggiungere un marker interface opzionale per Domain: `IMacelleriaDomainAssemblyMarker` etc. per riflessione.

---
## 5. Versioning API
- Strategia iniziale: path-based (`/v1`).
- Quando si introdurrà `/v2`, duplicare solo endpoints modificati (Backward compatibility).
- OpenAPI Title includa versione maggiore (`SpiedoBresciano API v1`).

---
## 6. OpenAPI & Documentazione
- Generare un singolo documento per major version.
- Server URL base `/` per compatibilità reverse proxy.
- Tag: usare il nome del modulo in PascalCase.
- Evitare esposizione tipi interni di dominio: utilizzare DTO nel Facade layer.

---
## 7. Logging
- Serilog con sink Console + File (rolling) + livello minimo `Information` (override `Microsoft.*` a `Warning`).
- Correlazione: includere trace/span id se OpenTelemetry attivo.

---
## 8. Telemetria (OpenTelemetry)
- Resource Attributes obbligatori: `service.name=SpiedoBrescianoApi`, `service.version=<assemblyVersion>`.
- Attivare modulo impostando `IsEnabled=true` + connection string Application Insights.
- Esportatori minimi: OTLP (futuro) + Azure Monitor.

---
## 9. Error Handling
- Standardizzare su `ProblemDetails` (RFC 7807).
- Mappare eccezioni di dominio (future) in 409 / 422 (a seconda dei casi) + codice custom in `problemDetails.Extensions["errorCode"]`.

---
## 10. Sicurezza
- Per ora anonimo (nessun auth); definire in futuro modulo `AuthenticationModule` per JWT.
- Prevedere hooking di policy via `AddAuthorization` nel futuro.

---
## 11. Health & Operatività
- Aggiungere /healthz (liveness) e /ready (readiness) (TODO).
- Metriche OTel automaticamente su `/metrics` se introdotto exporter Prometheus (non ancora richiesto).

---
## 12. Testing
- Architetturali: NetArchTest (già definiti) → aggiungere regole: “Domain non dipende da Infrastructure”.
- Futuro: Contract / Integration test per endpoints (WireMock/Minimal API test host).
- Coverage target minimo suggerito: 60% (da alzare progressivamente).

---
## 13. Build & CI (Futuro)
- GitHub Actions workflow: restore, build, test, publish (artifact swagger.json).
- Aggiungere caching NuGet.

---
## 14. Gestione Configurazioni
- `appsettings.json` + `appsettings.Development.json`.
- Connection strings in `ConnectionStrings:...`.
- Variabili per container: prefisso `SPIEDOBRESCIANO__` (compatibilità .NET config binder).

---
## 15. Placeholder / Boilerplate
- Evitare progetti *vuoti*: inserire marker class (es. `AssemblyInfoMarker`) se necessario.

---
## 16. Interfacce Facade
- Scopo: astrare registrazione servizi + orchestrare endpoints.
- Pattern consigliato:
  ```csharp
  public interface IMacelleriaFacade
  {
      void MapEndpoints(IEndpointRouteBuilder group);
      void RegisterServices(IServiceCollection services);
  }
  ```
- Implementazione concreta chiamata nel Module.

---
## 17. Cancellation & Async
- Ogni endpoint asincrono deve accettare `CancellationToken` finale.
- Nessun `.Result` / `.Wait()` ammesso.

---
## 18. Style & Qualità
- Nullable abilitato in tutti i progetti.
- Evitare commenti ovvi: usare commenti solo per scelte architetturali.
- Aggiungere analyzer (StyleCop / Roslyn) step successivo.

---
## 19. Commit / Branch Strategy
- Branch naming: `feature/<modulo>-<breve-descrizione>`.
- PR checklist: build ok, test pass, nessun nuovo warning, README aggiornato se necessario.

---
## 20. Manutenzione Template
- Annotare nel README la versione del template originale usata (commit hash) per eventuale diff update.

---
## 21. Alternative Architetturali (Opzionali)
| Opzione | Descrizione | Pro | Contro |
|---------|-------------|-----|--------|
| Vertical Slice | Cartelle per feature senza progetti multipli | Semplifica | Minor isolamento DDD |
| Source Generators Endpoints | Attributi generano mapping | Riduce boilerplate | Aumenta complessità toolchain |
| .NET Aspire | Orchestrazione osservabilità | Config facilitata | Sovra-ingegneria per MVP |

---
## 22. Roadmap Suggerita
1. Aggiungere interfacce Facade vuote.
2. Introdurre health checks.
3. Implementare error handling centralizzato (middleware).
4. Abilitare OpenTelemetry (trace + metrics) in Dev.
5. Aggiungere CI pipeline.
6. Definire primi casi d'uso (command/query) e DTO.

---
## 23. Checklist Rapida per Nuovi Contributor
- [ ] Clona repo e ripristina dipendenze `dotnet restore`.
- [ ] Esegui build `dotnet build`.
- [ ] Esegui test `dotnet test`.
- [ ] Avvia API `dotnet run --project src/SpiedoBresciano.Rest`.
- [ ] Verifica Swagger e Scalar UI.
- [ ] Non aggiungere riferimenti cross‑modulo non autorizzati.

---
## 24. Glossario Sintetico
- Facade: layer di esposizione endpoints + orchestrazione domain/query.
- SharedKernel (per modulo): tipi condivisi tra sub‑progetti dello stesso bounded context.
- Shared (globale): cross-cutting neutro (es. primitive, result pattern).
- ReadModel: proiezioni e query orientate a lettura (CQRS split).
- Infrastructure: implementazioni tecniche (repository, persistence, bus, ecc.).

---
## 25. Conformità Prompt Originale
Tutti i punti critici rimangono compatibili; questo documento aggiunge dettaglio operativo senza alterare le decisioni fondamentali.

---
## 26. Aggiornamento del Presente Documento
Ogni modifica architetturale sostanziale deve aggiornare: matrice dipendenze, roadmap, checklist.

---
Fine documento.
