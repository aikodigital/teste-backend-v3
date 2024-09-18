using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http.Json;
using TheatricalPlayersRefactoringKata.Application.Converters;
using TheatricalPlayersRefactoringKata.Application.Services.Statement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<TextStatementGenerator>();
builder.Services.AddTransient<XmlStatementGenerator>();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new GenderConverter());
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Teste Backend",
        Description = "Essa aplicação é usada por uma companhia de teatro para gerar extratos impressos a partir das faturas de seus clientes.",
        Contact = new OpenApiContact
        {
            Name = "Link projeto",
            Url = new Uri("https://github.com/aikodigital/teste-backend-v3")
        }
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
