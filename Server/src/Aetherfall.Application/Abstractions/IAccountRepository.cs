namespace Aetherfall.Application.Abstractions;

public interface IAccountRepository
{
    Task<AccountRecord?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(AccountRecord account, CancellationToken cancellationToken);
}

public sealed record AccountRecord(Guid Id, string Email, string PasswordHash);
