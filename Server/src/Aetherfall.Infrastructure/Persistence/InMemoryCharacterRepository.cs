using System.Collections.Concurrent;
using Aetherfall.Application.Abstractions;
using Aetherfall.Domain.Characters;

namespace Aetherfall.Infrastructure.Persistence;

public sealed class InMemoryCharacterRepository : ICharacterRepository
{
    private readonly ConcurrentDictionary<Guid, CharacterAggregate> _characters = new();

    public Task AddAsync(CharacterAggregate character, CancellationToken cancellationToken)
    {
        _characters[character.Id] = character;
        return Task.CompletedTask;
    }

    public Task<CharacterAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _characters.TryGetValue(id, out var character);
        return Task.FromResult(character);
    }

    public Task<IReadOnlyCollection<CharacterAggregate>> GetByAccountAsync(string accountId, CancellationToken cancellationToken)
    {
        var characters = _characters.Values.Where(x => x.AccountId == accountId).ToArray();
        return Task.FromResult<IReadOnlyCollection<CharacterAggregate>>(characters);
    }

    public Task UpdateAsync(CharacterAggregate character, CancellationToken cancellationToken)
    {
        _characters[character.Id] = character;
        return Task.CompletedTask;
    }
}
