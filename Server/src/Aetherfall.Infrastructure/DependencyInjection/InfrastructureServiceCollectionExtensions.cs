using Aetherfall.Application.Abstractions;
using Aetherfall.Application.World;
using Aetherfall.Infrastructure.Authentication;
using Aetherfall.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Aetherfall.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddAetherfallInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICharacterRepository, InMemoryCharacterRepository>();
        services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();
        services.AddSingleton<IQuestDefinitionRepository, InMemoryQuestDefinitionRepository>();
        services.AddSingleton<ICraftingRecipeRepository, InMemoryCraftingRecipeRepository>();
        services.AddSingleton<ICompanionDefinitionRepository, InMemoryCompanionDefinitionRepository>();
        services.AddSingleton<IZoneRepository, InMemoryZoneRepository>();
        services.AddSingleton<IPasswordHasher, Sha256PasswordHasher>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();
        return services;
    }
}
