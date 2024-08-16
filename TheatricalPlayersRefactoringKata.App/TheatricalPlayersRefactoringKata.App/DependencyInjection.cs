using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.App.AutoMapper;
using TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;
using TheatricalPlayersRefactoringKata.App.Validations.Plays.Register;

namespace TheatricalPlayersRefactoringKata.App;

public static class DependencyInjection
{
    public static void AddApp(this IServiceCollection services)
    {
        AddAutoMapper(services);

        AddValidations(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddValidations(IServiceCollection services)
    {
        services.AddScoped<IRegisterInvoiceValidation, RegisterInvoiceValidation>();
        services.AddScoped<IRegisterPlayValidation, RegisterPlayValidation>();
        services.AddScoped<IGetAllInvoiceValidation, GetAllInvoicesValidation>();
        services.AddScoped<IGetInvoiceByCustomerValidation, GeInvoiceByCustomerValidation>();
    }
}
