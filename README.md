# Aetherfall Online Vertical Slice

This repository now contains a production-oriented vertical slice scaffold for **Aetherfall Online**:

- Unity client folder structure under `Assets/Aetherfall`
- Clean Architecture backend under `Server/src`
- ScriptableObject-driven content definitions
- Domain/application/infrastructure/presentation separation
- In-memory backend vertical slice for characters, combat, quests, crafting, companions, world zone, and JWT auth
- Deterministic formula tests for core gameplay rules

## Solution

- `Aetherfall.slnx` - .NET solution
- `Server/src/Aetherfall.Api` - ASP.NET Core API scaffold
- `Server/src/Aetherfall.Domain` - entities, formulas, business rules
- `Server/src/Aetherfall.Application` - commands, handlers, interfaces
- `Server/src/Aetherfall.Infrastructure` - repositories, auth, networking foundation
- `Server/src/Aetherfall.Contracts` - transport contracts
- `Server/src/Aetherfall.Domain.Tests` - unit tests

## Vertical Slice Coverage

- Warrior and Mage foundations via class enum and client definitions
- Character creation and progression formulas
- Light/heavy attack, block, and dodge combat resolution
- Quest acceptance and objective advancement
- Inventory and crafting reward flow
- Companion recruitment scaffold
- Bastion Foothills zone with Ember Vault dungeon encounter
- JWT account registration/login scaffold
- Player-intent and replication network envelopes for server-authoritative expansion
