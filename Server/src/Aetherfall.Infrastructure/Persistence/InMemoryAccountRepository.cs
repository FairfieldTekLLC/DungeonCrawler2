using System.Collections.Concurrent;
using Aetherfall.Application.Abstractions;

namespace Aetherfall.Infrastructure.Persistence;

public sealed class InMemoryAccountRepository : IAccountRepository
{
    private readonly ConcurrentDictionary<string, AccountRecord> _accounts = new(StringComparer.OrdinalIgnoreCase);

    public Task<AccountRecord?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        _accounts.TryGetValue(email, out var account);
        return Task.FromResult(account);
    }

    public Task AddAsync(AccountRecord account, CancellationToken cancellationToken)
    {
        _accounts[account.Email] = account;
        return Task.CompletedTask;
    }
}
