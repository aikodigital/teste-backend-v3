using TheatricalPlayersAPI.Context;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using TheatricalPlayersAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add dbContext
builder.Services.AddDbContext<TheatricalDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Add controllers
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services
builder.Services.AddScoped<PlayServices>();
builder.Services.AddScoped<PerformanceServices>();
builder.Services.AddScoped<InvoiceServices>();


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
