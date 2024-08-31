using JogadoresTeatrais.Application.Interfaces;
using JogadoresTeatrais.Application.Service;
using JogadoresTeatrais.Domain.Interfaces;
using JogaresTeatrais.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JogadoresTeatrais.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IFaturaService, FaturaService>();

            services.AddScoped<IFaturaRepository, FaturaRepository>();

            services.AddScoped<IJogarRepository, JogarRepository>();
           

        }

    }
}
