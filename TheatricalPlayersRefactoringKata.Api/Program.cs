using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TheatricalPlayersRefactoringKata.Repository;
using TheatricalPlayersRefactoringKata.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the application container.
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("TheaterDb");

builder.Services.AddDbContext<RepositoryContext>(options => 
    options.UseSqlite(connectionString));

builder.Services.AddScoped<PerfomanceRepository>();
builder.Services.AddScoped<PerformanceService>();

builder.Services.AddEndpointsApiExplorer();

// Swagger Services Configuration
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Theater API",
            Description = "API para calcular valores de fatura e créditos para performances teatrais.",
            Contact = new OpenApiContact() { Name = "Isabella Chiara", Email = "isachiiara@gmail.com" }
        });
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TheaterSwaggerAnnotation.xml"));
    }
);

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            c.RoutePrefix = string.Empty; // Put Swagger UI at the host (localhost:5000)
        });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/swagger.xml", async context =>
{
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TheaterSwaggerAnnotation.xml");

    if (File.Exists(filePath))
    {
        context.Response.ContentType = "application/xml";
        await context.Response.SendFileAsync(filePath);
    }
    else
    {
        context.Response.StatusCode = 404;
    }
});

app.MapControllers();

app.Run();