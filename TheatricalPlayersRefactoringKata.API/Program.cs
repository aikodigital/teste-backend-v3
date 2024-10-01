using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TheatricalPlayersRefactoringKata.API.ProcessBackground;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;
using TheatricalPlayersRefactoringKata.Infra.Data.Context;
using TheatricalPlayersRefactoringKata.Infra.Data.Repositories;
using TheatricalPlayersRefactoringKata.Service.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("local");
    options.UseNpgsql(connectionString);
    options.UseLazyLoadingProxies();
});

services.AddHostedService<ProcessInvoiceQueue>();

services.AddScoped<IPlayRepository, PlayRepository>();
services.AddScoped<IInvoiceRepository, InvoiceRepository>();
services.AddScoped<ITypeGenreRepository, TypeGenreRepository>();
services.AddScoped<IPerfomanceRepository, PerformanceRepository>();
services.AddScoped<ICustomerStatementProcessRepository, CustomerStatementProcessRepository>();
services.AddScoped<ICustomerStatementRepository, CustomerStatementRepository>();
services.AddScoped<ICustomerPlaysStatementRepository, CustomerPlaysStatementRepository>();

services.AddScoped<IInvoiceService, InvoiceService>();
services.AddScoped<IStatementPrinterService, StatementPrinterService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();