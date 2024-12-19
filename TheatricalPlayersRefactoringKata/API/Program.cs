using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.API.Data;
using TheatricalPlayersRefactoringKata.API.Controllers;  
using TheatricalPlayersRefactoringKata.API.Queue;  

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TheaterAppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<ITheaterStatementProcessingQueue, StatementProcessingQueue>();

builder.Services.AddScoped<ITheaterStatementExportService, IStatementExportService>();
builder.Services.AddHostedService<StatementProcessorService>();
builder.Services.AddSingleton<ITheaterStatementProcessingQueue, StatementProcessingQueue>();

var services = builder.Services.AddControllers();

services.PartManager.ApplicationParts.Clear();

services.AddApplicationPart(typeof(StatementController).Assembly); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Theatrical Players Refactoring API",
        Version = "v1",
        Description = "API for processing theatrical performances and generating invoices"
    });

    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Theatrical Players Refactoring API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); 

app.Run();
