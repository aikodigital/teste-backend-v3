using TheatricalPlayersRefactoringKata.Api.Filters;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// If have an exception redirect to class ExceptionFilter
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

// Method of extension to add dependency injection of DbContext and Repositories
builder.Services.AddInfraestructure(builder.Configuration);

// Method of extension to add dependency injection of business rule
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
