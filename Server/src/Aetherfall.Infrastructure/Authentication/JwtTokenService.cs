using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aetherfall.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Aetherfall.Infrastructure.Authentication;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> IssueTokenAsync(string accountId, string email, CancellationToken cancellationToken)
    {
        var key = _configuration[JwtConstants.ConfigurationKey] ?? "AetherfallDevelopmentSigningKey123!";
        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: JwtConstants.Issuer,
            audience: JwtConstants.Audience,
            claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, accountId),
                new Claim(JwtRegisteredClaimNames.Email, email)
            },
            expires: DateTime.UtcNow.AddHours(JwtConstants.TokenExpirationHours),
            signingCredentials: credentials);

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
