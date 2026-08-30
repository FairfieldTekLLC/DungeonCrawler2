using Aetherfall.Domain.Characters;

namespace Aetherfall.Application.Abstractions;

public interface ICharacterRepository
{
    Task AddAsync(CharacterAggregate character, CancellationToken cancellationToken);
    Task<CharacterAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<CharacterAggregate>> GetByAccountAsync(string accountId, CancellationToken cancellationToken);
    Task UpdateAsync(CharacterAggregate character, CancellationToken cancellationToken);
}
