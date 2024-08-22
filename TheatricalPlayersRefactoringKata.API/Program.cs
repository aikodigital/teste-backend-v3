using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Application.DTOs.InvoiceDTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs.PerformanceDTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs.PlayDTOs;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Domain.Repositories;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;
using TheatricalPlayersRefactoringKata.Infrastructure.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Services.AddScoped<IPlayRepository, PlayRepository>();
builder.Services.AddScoped<IPlayService, PlayService>();
builder.Services.AddScoped<IPerformanceRepository, PerformanceRepository>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();


// Fluent Validation
builder.Services.AddScoped<IValidator<PlayRequest>, PlayValidation>();
builder.Services.AddScoped<IValidator<PerformanceRequest>, PerformanceValidation>();
builder.Services.AddScoped<IValidator<InvoiceRequest>, InvoiceValidation>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
