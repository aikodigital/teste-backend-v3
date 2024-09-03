using Main.Application;
using Main.Infrastructure;
using Main.Domain;
using Microsoft.OpenApi.Models;
using Main.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure()
        .AddDomain();
    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheatricalPlayersRefactoringKata", Version = "v1" });
        c.OperationFilter<AddHeaderOperationFilter>();
    });
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheatricalPlayersRefactoringKata v1");
    });
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}