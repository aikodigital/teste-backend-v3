using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Infrastructure;
using TheatricalPlayersRefactoringWebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<StatementService>();
builder.Services.AddTransient<StatementMapper>();
builder.Services.AddTransient<ITextStatementFormatter, TextStatementFormatter>();
builder.Services.AddTransient<IXmlStatementFormatter, XmlStatementFormatter>();
builder.Services.AddTransient<StatementPrinter>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

builder.Services.AddSwaggerGen(c =>
{
    c.ExampleFilters(); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
