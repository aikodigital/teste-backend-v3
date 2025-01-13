using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Infraestructure.Services.FileGenerator;

namespace TheatricalPlayersRefactoringKata.Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfraestructure(this IServiceCollection services)
        {
            AddServices(services);
        }

        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IFileGenerator, FileGenerator>();
        }
    }
}
