using Aetherfall.Application.Abstractions;
using Aetherfall.Application.Authentication;
using Aetherfall.Application.Characters;
using Aetherfall.Application.Combat;
using Aetherfall.Application.Companions;
using Aetherfall.Application.Crafting;
using Aetherfall.Application.Quests;
using Aetherfall.Application.World;
using Microsoft.Extensions.DependencyInjection;

namespace Aetherfall.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddAetherfallApplication(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<RegisterAccountCommand, Contracts.Authentication.AuthResponse>, RegisterAccountHandler>();
        services.AddScoped<ICommandHandler<LoginCommand, Contracts.Authentication.AuthResponse>, LoginHandler>();
        services.AddScoped<ICommandHandler<CreateCharacterCommand, Contracts.Characters.CharacterSummaryResponse>, CreateCharacterHandler>();
        services.AddScoped<ICommandHandler<AcceptQuestCommand, bool>, AcceptQuestHandler>();
        services.AddScoped<ICommandHandler<AdvanceQuestObjectiveCommand, bool>, AdvanceQuestObjectiveHandler>();
        services.AddScoped<ICommandHandler<CraftItemCommand, Contracts.Crafting.CraftItemResponse>, CraftItemHandler>();
        services.AddScoped<ICommandHandler<ResolveCombatCommand, Contracts.Combat.CombatResolutionResponse>, ResolveCombatHandler>();
        services.AddScoped<ICommandHandler<RecruitCompanionCommand, bool>, RecruitCompanionHandler>();
        services.AddScoped<WorldQueryService>();
        return services;
    }
}
