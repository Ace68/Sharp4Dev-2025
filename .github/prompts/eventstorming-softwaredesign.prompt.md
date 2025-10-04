# EventStorming Software Design Workshop

## Cos'è

L'**EventStorming Software Design** è un workshop collaborativo ideato
da Alberto Brandolini.\
È un approccio *visuale e partecipativo* per esplorare, comprendere e
modellare un dominio software complesso, partendo dagli eventi che
accadono nel sistema (domain events).

## Obiettivi

-   **Allineamento della conoscenza**: far emergere la comprensione
    condivisa tra stakeholder business, product owner, sviluppatori, UX,
    ecc.\
-   **Scoperta del dominio**: rendere visibili processi, flussi, regole
    e punti critici.\
-   **Design collaborativo**: arrivare a una rappresentazione del
    software più aderente al business.\
-   **Individuazione dei bounded context**: preparare la strada a
    un'architettura basata sul Domain-Driven Design.

## Struttura tipica di un workshop

1.  **Eventi di dominio (Domain Events)**
    -   I partecipanti scrivono su post-it arancioni gli eventi
        significativi che "sono accaduti" nel sistema ("Ordine creato",
        "Pagamento ricevuto", "Spedizione avviata"...).\
    -   Gli eventi sono disposti in ordine temporale su un grande spazio
        visivo (muro, lavagna, canvas).
2.  **Comandi (Commands)**
    -   Vengono individuati gli *stimoli* che hanno causato l'evento
        ("Crea Ordine", "Paga Ordine", "Spedisci merce").\
    -   Di solito rappresentati con post-it blu.
3.  **Aggregati / Entità**
    -   Si ragiona su quali entità del dominio gestiscono quei comandi e
        generano gli eventi (es. l'"Ordine" come aggregate root). POst-it gialli
4.  **Policy / Process Manager**
    -   Vengono introdotte le regole automatiche o processi che
        reagiscono agli eventi e generano nuovi comandi (post-it
        viola).
5.  **Read Models e UI**
    -   Si esplorano i modelli di lettura necessari e le interfacce
        utente che li utilizzano (post-it verdi).
6.  **Bounded Contexts**
    -   Si identificano sottosistemi coerenti e autonomi (es. "Gestione
        Ordini", "Pagamenti", "Spedizioni").\
    -   Questa fase è cruciale per l'architettura software.
7.  **Hot Spots / Problemi aperti**
    -   Vengono evidenziate le aree di incertezza o conflitto (post-it
        rossi), per indirizzare conversazioni future.

## Output di un workshop

-   Una mappa visiva degli eventi e processi del dominio.\
-   Una comprensione condivisa tra team tecnico e business.\
-   Bozze di design software orientate a DDD (aggregati, bounded
    contexts, flussi di eventi).\
-   Una lista di aree di rischio o punti che richiedono ulteriori
    chiarimenti.

## Caratteristiche fondamentali

-   **Collaborativo**: tutti contribuiscono, non solo sviluppatori.\
-   **Visivo**: tutto su un grande supporto, con post-it colorati.\
-   **Iterativo**: si parte dall'alto livello (Big Picture), poi si
    scende nel dettaglio (Design-level).\
-   **Empirico**: non si cerca la perfezione subito, ma si esplora e si
    adatta.
