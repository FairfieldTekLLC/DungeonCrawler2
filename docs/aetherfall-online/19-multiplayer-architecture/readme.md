# Multiplayer Architecture

Aetherfall uses dedicated authoritative zone servers, instance servers, and backend services.

### Runtime Topology

- **Gateway:** authentication handoff, routing, rate limits, and session tokens.
- **World Service:** character location, shard selection, presence, matchmaking.
- **Zone Servers:** open-world simulation, NPCs, events, combat authority.
- **Instance Servers:** dungeons, raids, housing interiors, solo story phases.
- **Chat Service:** channels, moderation, guild chat, proximity chat.
- **Economy Service:** auction house, contracts, trade settlement.
- **Guild Service:** roster, permissions, projects, banks, rankings.
- **Inventory Service:** item ownership, durability, bank, mail, anti-duplication.
- **Telemetry Service:** metrics, cheat signals, balance data.

Party size supports 2-8 players. Raid sizes support 5, 10, and 20 players. Shared events and world bosses scale by participant count and average item power.
