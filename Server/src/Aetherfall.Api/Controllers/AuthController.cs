using Aetherfall.Application.Abstractions;
using Aetherfall.Application.Authentication;
using Aetherfall.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Aetherfall.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> RegisterAsync(
        [FromBody] RegisterRequest request,
        [FromServices] ICommandHandler<RegisterAccountCommand, AuthResponse> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new RegisterAccountCommand(request.Email, request.Password), cancellationToken);
        return result.Succeeded ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> LoginAsync(
        [FromBody] LoginRequest request,
        [FromServices] ICommandHandler<LoginCommand, AuthResponse> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new LoginCommand(request.Email, request.Password), cancellationToken);
        return result.Succeeded ? Ok(result.Value) : Unauthorized(result.Error);
    }
}
