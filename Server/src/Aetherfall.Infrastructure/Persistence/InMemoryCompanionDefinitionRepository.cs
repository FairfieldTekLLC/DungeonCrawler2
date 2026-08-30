using Aetherfall.Application.Abstractions;
using Aetherfall.Domain.Characters;
using Aetherfall.Domain.Companions;

namespace Aetherfall.Infrastructure.Persistence;

public sealed class InMemoryCompanionDefinitionRepository : ICompanionDefinitionRepository
{
    public Task<CompanionAggregate?> CreateAsync(string companionDefinitionId, CancellationToken cancellationToken)
    {
        CompanionAggregate? companion = companionDefinitionId switch
        {
            "companion.lyra" => new CompanionAggregate(Guid.NewGuid(), companionDefinitionId, new CharacterAttributes(8, 10, 16, 9, 14, 7), new PersonalityProfile(75, 68, 71, 40, 73)),
            _ => null
        };

        return Task.FromResult(companion);
    }
}
