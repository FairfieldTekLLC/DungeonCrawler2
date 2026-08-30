using Aetherfall.Domain.Companions;

namespace Aetherfall.Application.Abstractions;

public interface ICompanionDefinitionRepository
{
    Task<CompanionAggregate?> CreateAsync(string companionDefinitionId, CancellationToken cancellationToken);
}
