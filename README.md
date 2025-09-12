"# Shared Shopping" 

MVP Feature List
=====================
User Features
    Register / Login (basic auth, admin role only).
    Create shared shopping list (one list per group).
    Add / edit / remove items in the list.
    View cheapest prices per item (fetched from APIs).
    Offline storage of shopping list (localStorage / IndexedDB).

Backend Features
    CQRS split:
        Commands: AddItem, RemoveItem, UpdateItem.
        Queries: GetList, GetItemWithPrice.
    Price fetching worker:
        Periodically fetch prices from APIs.
        Publish/consume Kafka events for updates.
    Caching layer:
        Store latest prices in Redis for fast reads.

System Features
    API Gateway (routing requests to Command/Query services).
    Logging with Serilog + Seq (trace user actions, price updates).
    Basic authentication & token issuance (JWT).
    Docker Compose setup (services + DB + Redis + Kafka + Seq).
    CI/CD pipeline skeleton (build, test, containerize, deploy).

shared-shopping/
├─ src/
│  ├─ Api.Gateway/           # (YARP or tiny ASP.NET gateway)
│  ├─ Service.Command/       # write-side API (CQRS Commands)
│  ├─ Service.Query/         # read-side API (CQRS Queries)
│  ├─ Worker.PriceFetcher/   # background worker (Kafka consumer)
│  ├─ Core.Domain/           # domain models, DTOs, interfaces
│  ├─ Infrastructure.Data/   # EF Core DbContext, migrations
│  └─ Tests/                 # unit/integration test projects
├─ frontend/                 # React + TypeScript (Vite)
├─ docker/                   # docker-compose files, helper scripts
└─ .github/workflows/        # CI skeleton

                   ┌───────────────────────────┐
                   │         Frontend           │
                   │   React (Web / Mobile PWA) │
                   └───────────────┬───────────┘
                                   │
                                   ▼
                         ┌─────────────────┐
                         │   API Gateway   │
                         └───────┬─────────┘
                                 │
          ┌──────────────────────┼─────────────────────────┐
          │                      │                         │
          ▼                      ▼                         ▼
     ┌─────────────────┐     ┌─────────────────┐     ┌──────────────────┐
     │ Command Service │     │  Query Service  │     │ Price Fetcher    │
     │  (CQRS Writes)  │     │  (CQRS Reads)   │     │  Worker Service  │
     └───────┬─────────┘     └────────┬────────┘     └────────┬─────────┘
             │                        │                       │
             ▼                        ▼                       │
     ┌─────────────┐          ┌─────────────┐                 │
     │   MS SQL    │ <------> │   Redis     │ <---------------┘
     │  (storage)  │   Cache  │  (cache)    │   Kafka events
     └─────────────┘          └─────────────┘

               ┌────────────────────────────┐
               │   External Price APIs      │
               │ (JSON responses from shops)│
               └────────────────────────────┘

