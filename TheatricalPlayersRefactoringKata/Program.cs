using TheatricalPlayersRefactoringKata.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.InitialiseDatabaseAsync();
}

app.Map("/", () => Results.Redirect("/swagger"));

// Configure the APIs
app.MapExtract();
app.MapStatement();

app.Run();
