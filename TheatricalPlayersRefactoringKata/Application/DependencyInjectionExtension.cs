using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Application.Services.AutoMapper;
using TheatricalPlayersRefactoringKata.Application.UseCases.Extract;
using TheatricalPlayersRefactoringKata.Application.UseCases.Invoice;

namespace TheatricalPlayersRefactoringKata.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUserCase(services);
    }
    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddScoped(options => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper());


    }
    private static void AddUserCase(IServiceCollection services)
    {
        services.AddScoped<IProcessInvoiceUseCase, ProcessInvoiceUseCase>();
        services.AddScoped<IExtractInvoiceUseCase, ExtractInvoiceUseCase>();
    }


}
