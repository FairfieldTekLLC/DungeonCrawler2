# Server Architecture

Dedicated servers run authoritative simulation ticks. Clients submit intent, never final results. Servers validate movement, cooldowns, resources, line of sight, target legality, inventory ownership, and encounter state before replication.

### Services

- ASP.NET Core account, character, guild, economy, and inventory APIs.
- Realtime zone/instance processes for combat and world state.
- PostgreSQL for persistence, Redis for ephemeral locks and matchmaking, object storage for logs and content manifests.
- Message bus for economy settlements, mail, guild notifications, telemetry, and seasonal jobs.

### Scaling

Shard by region and zone. Dynamic instance allocation for dungeons, raids, and housing. World bosses run on reserved zone workers. Guild wars reserve territory simulation nodes.
