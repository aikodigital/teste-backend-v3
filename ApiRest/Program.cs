using Microsoft.OpenApi.Models;
using TheatricalPlayersRefactoringKata.infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Theatrical Players API",
        Version = "v1",
        Description = "API for managing theatrical players, performances, and invoices.",
        Contact = new OpenApiContact
        {
            Name = "Hugo Vidal Alves Rezende",
            Email = "seuemail@dominio.com"
        }
    });
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddDbContext<ApiDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Theatrical Players API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
