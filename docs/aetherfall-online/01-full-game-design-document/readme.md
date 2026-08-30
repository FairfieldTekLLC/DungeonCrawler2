# Full Game Design Document

**Title:** Aetherfall Online  
**Genre:** Fantasy MMORPG / real-time action RPG  
**Platform:** PC  
**Engine:** Unity  
**Language:** C#  
**Networking:** Dedicated authoritative servers using Mirror or Unity Netcode for GameObjects  
**Backend:** ASP.NET Core services, PostgreSQL persistence, Redis cache, cloud hosted on Azure or AWS

Aetherfall Online is a shared-world fantasy MMORPG set in Elyndor, a fractured world recovering from the divine catastrophe known as The Sundering. Players explore five major continents, join ideological factions, form parties and guilds, craft evolving legendary equipment, command AI companions, and participate in scalable dungeons, raids, world bosses, trade systems, housing, and territory conflicts.

### Design Pillars

1. **Living World:** Dynamic events, faction control, seasonal arcs, ecology simulation, and persistent player impact.
2. **Deep Progression:** Race, class, specialization, talents, professions, factions, guilds, companions, housing, and legendary item growth.
3. **Action Combat:** Light/heavy attacks, blocking, dodging, parrying, target lock, combos, crowd control, status effects, ultimates, and multi-phase bosses.
4. **Player Economy:** Crafting, contracts, auction house, player shops, guild commerce, dynamic prices, and regional supply constraints.
5. **AI Companions:** Personality traits, memory, loyalty, relationships, class/profession trees, emotional state, tactical decisions, and synergies.
6. **Commercial Scalability:** Clean architecture, ScriptableObject data, dependency injection, dedicated server authority, telemetry, anti-cheat, and content pipelines.

### Core Player Loop

Explore zone -> complete quests/events -> gather resources -> fight enemies/bosses -> earn loot/currency/reputation -> craft/enchant/upgrade gear -> improve class, faction, companion, guild, and profession progression -> unlock harder dungeons, raids, territory objectives, and legendary hunts.

### Content Cadence

- Weekly rotating contracts, world events, and economy modifiers.
- Monthly dungeon affixes and faction campaigns.
- Quarterly raid wings, guild projects, and legendary hunts.
- Annual continent-scale expansion chapters.
