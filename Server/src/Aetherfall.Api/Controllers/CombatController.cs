using Aetherfall.Application.Abstractions;
using Aetherfall.Application.Combat;
using Aetherfall.Contracts.Combat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aetherfall.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/combat")]
public sealed class CombatController : ControllerBase
{
    [HttpPost("resolve")]
    public async Task<ActionResult<CombatResolutionResponse>> ResolveAsync([FromBody] ResolveCombatRequest request, [FromServices] ICommandHandler<ResolveCombatCommand, CombatResolutionResponse> handler, CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new ResolveCombatCommand(request.ActionType, request.AttackerId, request.DefenderId, request.CritSeed), cancellationToken);
        return result.Succeeded ? Ok(result.Value) : BadRequest(result.Error);
    }
}
