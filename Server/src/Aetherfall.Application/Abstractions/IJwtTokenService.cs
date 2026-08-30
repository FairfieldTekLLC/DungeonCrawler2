namespace Aetherfall.Application.Abstractions;

public interface IJwtTokenService
{
    Task<string> IssueTokenAsync(string accountId, string email, CancellationToken cancellationToken);
}
