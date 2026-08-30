# Production-Ready C# Architecture

Use Clean Architecture boundaries:

- **Domain:** entities, value objects, combat formulas, crafting rules, quest state machines.
- **Application:** use cases, commands, validators, interfaces, DTOs.
- **Infrastructure:** PostgreSQL repositories, Redis, cloud queues, telemetry, auth integrations.
- **Presentation:** Unity client controllers, server hubs, ASP.NET Core endpoints.

Principles:

- SOLID, dependency injection, async persistence, deterministic combat simulation, idempotent backend commands, input validation, server authority, testable pure formulas, and content data validation.
- Avoid singletons except composition roots and Unity bootstrap adapters.
- Use event-driven systems for combat, quest updates, inventory changes, crafting completion, faction reputation, and companion memories.

Example application command:

```csharp
public sealed record CraftItemCommand(
    Guid CharacterId,
    string RecipeId,
    IReadOnlyList<Guid> MaterialItemIds,
    string StationId);

public interface ICraftingService
{
    Task<CraftItemResult> CraftAsync(CraftItemCommand command, CancellationToken cancellationToken);
}
```
