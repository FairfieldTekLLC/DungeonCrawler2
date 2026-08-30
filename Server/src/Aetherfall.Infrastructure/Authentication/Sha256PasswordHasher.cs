using System.Security.Cryptography;
using System.Text;
using Aetherfall.Application.Abstractions;

namespace Aetherfall.Infrastructure.Authentication;

public sealed class Sha256PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }

    public bool Verify(string password, string hash)
        => string.Equals(Hash(password), hash, StringComparison.Ordinal);
}
