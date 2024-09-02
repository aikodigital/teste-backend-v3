using Asp.Versioning.ApiExplorer;
using TheatricalPlayersRefactoringKata.API.Extensions;
using TheatricalPlayersRefactoringKata.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure environment settings
// and SQL Server for data storage
builder
    .ConfigureEnvironmentSettings()
    .ConfigureSqlServer()
    .ConfigureRabbitMq();

// Add API versioning
// and Swagger configuration
builder
    .Services
    .ConfigureApiVersioning()
    .ConfigureSwagger();

// Add services for statement processor
// and repositories for data access
builder
    .Services
    .AddStatementProcessorServices()
    .AddRepositories()
    .AddServices();

// You can add additional logging providers,
// such as file logging or external logging services if you want.
builder
    .Services
    .AddLogging(configure => configure.AddConsole());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        //configure swagger to show all versions
        var provider = app.Services
            .GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"TheatricalPlayers API {description.GroupName.ToUpperInvariant()}");
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Register middleware for handling exceptions
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
