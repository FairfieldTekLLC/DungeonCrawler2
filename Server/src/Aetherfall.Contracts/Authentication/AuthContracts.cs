namespace Aetherfall.Contracts.Authentication;

public sealed record RegisterRequest(string Email, string Password);
public sealed record LoginRequest(string Email, string Password);
public sealed record AuthResponse(Guid AccountId, string Email, string AccessToken);
