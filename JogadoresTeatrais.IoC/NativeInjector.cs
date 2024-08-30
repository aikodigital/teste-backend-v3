using JogadoresTeatrais.Application.Interfaces;
using JogadoresTeatrais.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace JogadoresTeatrais.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IFaturaService, FaturaService>();
           

        }

    }
}
