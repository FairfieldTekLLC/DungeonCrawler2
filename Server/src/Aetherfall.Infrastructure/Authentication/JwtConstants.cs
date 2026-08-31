namespace Aetherfall.Infrastructure.Authentication;

/// <summary>
/// Centralized constants for JWT token configuration and generation.
/// </summary>
public static class JwtConstants
{
    /// <summary>
    /// Configuration key for JWT signing key in appsettings.json.
    /// </summary>
    public const string ConfigurationKey = "Authentication:JwtKey";

    /// <summary>
    /// JWT token issuer identifier.
    /// </summary>
    public const string Issuer = "Aetherfall.Api";

    /// <summary>
    /// JWT token audience identifier.
    /// </summary>
    public const string Audience = "Aetherfall.Client";

    /// <summary>
    /// Default token expiration time in hours.
    /// </summary>
    public const int TokenExpirationHours = 8;
}
