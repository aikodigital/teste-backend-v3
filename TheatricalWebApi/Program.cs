using System.Globalization;
using Microsoft.AspNetCore.Localization;
using TheatricalPlayersRefactoringKata.Interfaces;
using TheatricalPlayersRefactoringKata.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<XmlStatementFormatter>();
builder.Services.AddScoped<TextStatementFormatter>();


var app = builder.Build();


app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(CultureInfo.InvariantCulture),
    SupportedCultures = new[] { CultureInfo.InvariantCulture },
    SupportedUICultures = new[] { CultureInfo.InvariantCulture }
});

// Configurações de ambiente
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
