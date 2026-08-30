using Aetherfall.Application.Abstractions;
using Aetherfall.Application.Crafting;
using Aetherfall.Contracts.Crafting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aetherfall.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/crafting")]
public sealed class CraftingController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CraftItemResponse>> CraftAsync([FromBody] CraftItemRequest request, [FromServices] ICommandHandler<CraftItemCommand, CraftItemResponse> handler, CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new CraftItemCommand(request.CharacterId, request.RecipeId, request.MaterialQuality, request.SpecializationBonus, request.StationQuality, request.RandomRoll), cancellationToken);
        return result.Succeeded ? Ok(result.Value) : BadRequest(result.Error);
    }
}
