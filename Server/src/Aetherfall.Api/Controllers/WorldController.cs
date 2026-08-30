using Aetherfall.Application.World;
using Aetherfall.Contracts.World;
using Microsoft.AspNetCore.Mvc;

namespace Aetherfall.Api.Controllers;

[ApiController]
[Route("api/world")]
public sealed class WorldController : ControllerBase
{
    [HttpGet("zones")]
    public async Task<ActionResult<IReadOnlyCollection<ZoneResponse>>> GetZonesAsync([FromServices] WorldQueryService queryService, CancellationToken cancellationToken)
    {
        var zones = await queryService.GetZonesAsync(cancellationToken);
        return Ok(zones);
    }
}
