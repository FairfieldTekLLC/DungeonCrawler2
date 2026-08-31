using System.Text.RegularExpressions;
using Aetherfall.Application.Abstractions;
using Aetherfall.Application.Common;
using Aetherfall.Contracts.Authentication;

namespace Aetherfall.Application.Authentication;

public sealed record RegisterAccountCommand(string Email, string Password);
public sealed record LoginCommand(string Email, string Password);

public sealed class RegisterAccountHandler : ICommandHandler<RegisterAccountCommand, AuthResponse>
{
    private static readonly Regex EmailRegex = new(
        ValidationConstants.EmailRegexPattern,
        RegexOptions.Compiled | RegexOptions.IgnoreCase,
        TimeSpan.FromMilliseconds(100));

    private readonly IAccountRepository _accounts;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public RegisterAccountHandler(IAccountRepository accounts, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
    {
        _accounts = accounts;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<AuthResponse>> HandleAsync(RegisterAccountCommand command, CancellationToken cancellationToken)
    {
        if (!IsValidEmail(command.Email)) return Result<AuthResponse>.Failure("Invalid email address.");
        if (string.IsNullOrWhiteSpace(command.Password) || command.Password.Length < ValidationConstants.MinPasswordLength) 
            return Result<AuthResponse>.Failure($"Password must be at least {ValidationConstants.MinPasswordLength} characters.");

        var existing = await _accounts.GetByEmailAsync(command.Email, cancellationToken);
        if (existing is not null) return Result<AuthResponse>.Failure("Account already exists.");

        var account = new AccountRecord(Guid.NewGuid(), command.Email.Trim().ToLowerInvariant(), _passwordHasher.Hash(command.Password));
        await _accounts.AddAsync(account, cancellationToken);
        var token = await _jwtTokenService.IssueTokenAsync(account.Id.ToString(), account.Email, cancellationToken);
        return Result<AuthResponse>.Success(new AuthResponse(account.Id, account.Email, token));
    }

    private static bool IsValidEmail(string email) 
        => !string.IsNullOrWhiteSpace(email) && EmailRegex.IsMatch(email);
}

public sealed class LoginHandler : ICommandHandler<LoginCommand, AuthResponse>
{
    private readonly IAccountRepository _accounts;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginHandler(IAccountRepository accounts, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
    {
        _accounts = accounts;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<AuthResponse>> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        var account = await _accounts.GetByEmailAsync(command.Email.Trim().ToLowerInvariant(), cancellationToken);
        if (account is null || !_passwordHasher.Verify(command.Password, account.PasswordHash))
        {
            return Result<AuthResponse>.Failure("Invalid credentials.");
        }

        var token = await _jwtTokenService.IssueTokenAsync(account.Id.ToString(), account.Email, cancellationToken);
        return Result<AuthResponse>.Success(new AuthResponse(account.Id, account.Email, token));
    }
}
