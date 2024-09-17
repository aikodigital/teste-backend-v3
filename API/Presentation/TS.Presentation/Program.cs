using System.Reflection;
using System.Text.Json.Serialization;
using TS.Domain.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TS.Domain.Repositories.Invoices;
using TS.Domain.Repositories.Customers;
using TS.Domain.Repositories.Performances;
using TS.Domain.Repositories.Plays;
using Hangfire;
using TS.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var appAssembly = Assembly.Load("TS.Application");

builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(appAssembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Description = "Theatrical Solutions Api"
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TS"));
});

builder.Services.AddScoped<IInvoicesRepository, InvoicesRepository>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IPerformancesRepository, PerformancesRepository>();
builder.Services.AddScoped<IPlaysRepository, PlaysRepository>();
builder.Services.AddScoped<IRabbitMQServices, RabbitMQServices>();

builder.Services.AddMvc()
                .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

GlobalConfiguration.Configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("TS"));

builder.Services.AddHangfire(Configuration => Configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("TS")));

builder.Services.AddHangfireServer();
RecurringJob.AddOrUpdate<IRabbitMQServices>("Faturas", services => services.Consumer(), "*/2 * * * *"); //Expressão CROM para executar a cada 2 minutos

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
    app.UseHangfireDashboard();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints(endpoints =>
     {
         _ = endpoints.MapControllers();
         _ = endpoints.MapHangfireDashboard();
     });

app.Run();