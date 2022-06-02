using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.App;
using TheatricalPlayersRefactoringKata.App.Interfaces;
using TheatricalPlayersRefactoringKata.CrossCutting.Mapper;
using TheatricalPlayersRefactoringKata.Domain.Interface.Repository;
using TheatricalPlayersRefactoringKata.Domain.Interface.Services;
using TheatricalPlayersRefactoringKata.Domain.Interface.UoW;
using TheatricalPlayersRefactoringKata.Domain.Service;
using TheatricalPlayersRefactoringKata.Repository;
using TheatricalPlayersRefactoringKata.Repository.Configuration.Context;
using TheatricalPlayersRefactoringKata.Repository.UoW;

namespace TheatricalPlayersRefactoringKata.CrossCutting.IoC
{
    public static class DependencyInjector
    {
        public static IServiceCollection AddTheatricalPlayersIoC(this IServiceCollection services)
        {
            services.AddMapperConfiguration();
            services.AddTheatricalPlayersApplication();
            services.AddTheatricalPlayersService();
            services.AddTheatricalPlayersRepository();
            services.AddTheatricalPlayersDataContext();

            return services;
        }

        public static IServiceCollection AddTheatricalPlayersApplication(this IServiceCollection services)
        {
            services.AddTransient<IInvoiceApp, InvoiceApp>();

            return services;
        }

        public static IServiceCollection AddTheatricalPlayersService(this IServiceCollection services)
        {
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<ICalculateService, CalculateService>();

            return services;
        }

        public static IServiceCollection AddTheatricalPlayersRepository(this IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IPerformanceRepository, PerformanceRepository>();
            services.AddTransient<IPlayRepository, PlayRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddTheatricalPlayersDataContext(this IServiceCollection services)
        {
            services.AddDbContext<TheatricalContext>();
            services.AddEntityFrameworkSqlServer();

            services.AddScoped<ITheatricalContext, TheatricalContext>();

            return services;
        }
    }
}