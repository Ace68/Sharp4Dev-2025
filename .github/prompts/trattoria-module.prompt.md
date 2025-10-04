## Business Logic
In questa fase non preoccuparti di implementare IRepository, l'intrefaccia che trovi in Muflone. Esiste un package apposito che lo implementa già https://www.nuget.org/packages/Muflone.Eventstore.gRPC. Trovi il repo qui https://github.com/CQRS-Muflone/Muflone.Eventstore.gRPC.
Nel ReadModel non ti preoccupare di implementare il database. Limitati a prepare i relativi EventHandler per gli eventi di dominio, che aggiorneranno i DTOs.
Per ogni command che prepari, implementa il relativo CommandHandler. Un solo command handler per ogni comando.

## Progetto Facade
- In questo progetto implementa gli endpoints, e registra i servizi necesssari nel relativo file helper.
- Implementa i validators, utilizzando FluentValidation, nella relativa cartella Validators nel progetto.

## Progetto Domain
In questo progetto implementa 
- I CommandHandler utilizzando la libreria Muflone (https://github.com/CQRS-Muflone/Muflone) e le istruzioni che trovi nel file muflone-core.prompt.md.
- Le entità di dominio, utilizzando la classe abstract AggregateRoot per gli aggregati, EntityBase per le Entità e ValueObject per i Value Objects.
- Utilizza il CommandHandler che trovi nel repository di esempio.

# Progetto Shared
Utilizza il progetto shared del modulo per 
- Definire, nella cartella Messages, i comandi (cartella Commands) e gli eventi (cartella Events) utilizzando le classi Command e DomainEvent che trovi nella libreria Muflone.
- Definire tipi custom nella cartella CustomTypes. Non utilizzare tipi primitivi per definire le proprietà degli oggetti, ma scrivi dei tipi custom utilizzando Record e classi base di Muflone, come ad esempio:
  - SpiedoId : DomainId
  - Bird: Record

## Progetto Infrastructure
Registra il repository di Muflone che utilizza Kurrent (formerly EventStore) come da repository https://github.com/CQRS-Muflone/Muflone.Eventstore.gRPC.

## Progetto ReadModel
In questo progetto 
- Implementa i DTOs per la parte di lettura dei dati (ReadModel), possibilmente utilizzando MongoDB come database.
- Implementa gli handlers per gli eventi, che aggiornano i DTOs, nella cartella EventHandlers.
