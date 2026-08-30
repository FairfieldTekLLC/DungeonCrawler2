using Aetherfall.Application.Abstractions;
using Aetherfall.Application.Characters;
using Aetherfall.Application.Companions;
using Aetherfall.Application.Quests;
using Aetherfall.Contracts.Characters;
using Aetherfall.Contracts.Companions;
using Aetherfall.Contracts.Quests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aetherfall.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/characters")]
public sealed class CharactersController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CharacterSummaryResponse>> CreateAsync(
        [FromBody] CreateCharacterRequest request,
        [FromServices] ICommandHandler<CreateCharacterCommand, CharacterSummaryResponse> handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateCharacterCommand(request.AccountId, request.Name, request.ClassType, request.Strength, request.Dexterity, request.Intelligence, request.Vitality, request.Wisdom, request.Luck);
        var result = await handler.HandleAsync(command, cancellationToken);
        return result.Succeeded ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("quests/accept")]
    public async Task<ActionResult> AcceptQuestAsync([FromBody] AcceptQuestRequest request, [FromServices] ICommandHandler<AcceptQuestCommand, bool> handler, CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new AcceptQuestCommand(request.CharacterId, request.QuestDefinitionId), cancellationToken);
        return result.Succeeded ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("quests/advance")]
    public async Task<ActionResult> AdvanceQuestAsync([FromBody] AdvanceQuestObjectiveRequest request, [FromServices] ICommandHandler<AdvanceQuestObjectiveCommand, bool> handler, CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new AdvanceQuestObjectiveCommand(request.CharacterId, request.QuestDefinitionId, request.ObjectiveId, request.Amount), cancellationToken);
        return result.Succeeded ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("companions/recruit")]
    public async Task<ActionResult> RecruitCompanionAsync([FromBody] RecruitCompanionRequest request, [FromServices] ICommandHandler<RecruitCompanionCommand, bool> handler, CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new RecruitCompanionCommand(request.CharacterId, request.CompanionDefinitionId), cancellationToken);
        return result.Succeeded ? Ok() : BadRequest(result.Error);
    }
}
