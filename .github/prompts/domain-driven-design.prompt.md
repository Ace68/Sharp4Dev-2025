## Istruzioni Domain-Driven Design (DDD)

### Principi Generali
- Applica rigorosamente i pattern del Domain-Driven Design in tutto il codice generato
- Mantieni una separazione netta tra i layer architetturali (Domain, Application, Infrastructure, Presentation)
- Usa l'Ubiquitous Language del dominio nei nomi di classi, metodi e proprietà

### Strategic Design
- Ogni Bounded Context ({ModuleName}) deve essere completamente isolato dagli altri
- Non creare dipendenze dirette tra Bounded Context diversi
- Usa le interfacce Facade per esporre solo le funzionalità necessarie verso l'esterno
- Mantieni la Context Map attraverso le interfacce dei Facade

### Tactical Design Patterns
- **Entities**: Devono avere un'identità unica e persistente, implementare l'equality basata sull'ID
- **Value Objects**: Devono essere immutabili, implementare l'equality basata sui valori, non avere identità
- **Aggregates**: Definire chiaramente l'Aggregate Root, mantenere l'invarianza dei dati, accesso solo tramite root
- **Domain Services**: Per logiche di dominio che non appartengono a singole entità
- **Repositories**: Solo interfacce nel Domain layer, implementazioni nell'Infrastructure
- **Domain Events**: Per comunicare cambiamenti significativi nel dominio

### Struttura dei Layer
- **Domain Layer**: Solo entità, value objects, domain services, repository interfaces, domain events. Nessuna dipendenza esterna
- **Application Layer**: Use cases, command/query handlers, application services. Dipende solo dal Domain
- **Infrastructure Layer**: Implementazioni concrete di repository, servizi esterni, persistenza
- **Facade Layer**: Espone endpoint e coordina Application services. Non contiene logica di business

### Regole di Dipendenza
- Il Domain layer non deve dipendere da nessun altro layer
- L'Application layer può dipendere solo dal Domain
- L'Infrastructure implementa le interfacce definite nel Domain
- I Facade possono dipendere da Application e Domain, ma non da Infrastructure direttamente

### Naming Conventions
- Usa il linguaggio del dominio: `Carne`, `Spiedo`, `Cottura`, `Ordinazione`
- Le entità hanno nomi che riflettono concetti di business
- I servizi hanno nomi che descrivono azioni di dominio
- Gli eventi usano il past tense: `CarneAggiuntaAlloSpiedo`, `CotturaCompletata`