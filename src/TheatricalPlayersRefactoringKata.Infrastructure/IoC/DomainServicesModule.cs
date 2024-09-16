using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Services.PlayAmount;
using TheatricalPlayersRefactoringKata.Services.PlayVolumeCredits;

namespace TheatricalPlayersRefactoringKata.Infrastructure.IoC;

public static class DomainServicesModule
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<IPlayAmountService, PlayAmountService>();
        services.AddSingleton<IPlayVolumeCreditsService, PlayVolumeCreditsService>();
    }
}