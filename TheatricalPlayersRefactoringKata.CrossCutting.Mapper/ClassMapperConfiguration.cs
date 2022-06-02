using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace TheatricalPlayersRefactoringKata.CrossCutting.Mapper
{
    public static class ClassMapperConfiguration
    {
        public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
        {
            IMapper mapper = RegisterMappings().CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }

        private static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile(new DomainToViewModelProfile());
                config.AddProfile(new ViewModelToDomainProfile());
            });
        }
    }
}