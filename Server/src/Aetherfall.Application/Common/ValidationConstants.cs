namespace Aetherfall.Application.Common;

/// <summary>
/// Centralized validation constants used across the application layer.
/// </summary>
public static class ValidationConstants
{
    /// <summary>
    /// Minimum required length for user passwords.
    /// </summary>
    public const int MinPasswordLength = 8;

    /// <summary>
    /// Minimum required length for character names.
    /// </summary>
    public const int MinCharacterNameLength = 3;

    /// <summary>
    /// Regular expression pattern for validating email addresses.
    /// Matches standard email format: localpart@domain.extension
    /// </summary>
    public const string EmailRegexPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
}
