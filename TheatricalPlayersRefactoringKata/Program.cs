using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Services;
using TheatricalPlayersRefactoringKata.Infrastructure;
using TheatricalPlayersRefactoringKata.Infrastructure.Configuration;
using TheatricalPlayersRefactoringKata.Infrastructure.Services;
using TheatricalPlayersRefactoringKata.Infrastructure.Converters;
using TheatricalPlayersRefactoringKata.Core.UseCases;

namespace TheatricalPlayersRefactoringKata
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5001);
            });

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new PlayConverter());
                });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API dos Jogadores Teatrais", Version = "v1" });
            });

            builder.Services.AddScoped<IPerformanceCalculator, TragedyCalculator>();
            builder.Services.AddScoped<IPerformanceCalculator, ComedyCalculator>();
            builder.Services.AddScoped<IPerformanceCalculator, HistoricalCalculator>();

            builder.Services.AddScoped<StatementGenerator, TextStatementGenerator>();
            builder.Services.AddScoped<StatementGenerator, XmlStatementGenerator>();

            builder.Services.AddScoped<Func<string, IStatementGenerator>>(sp => key =>
            {
                return key.ToLower() switch
                {
                    "text" => sp.GetRequiredService<TextStatementGenerator>() as IStatementGenerator,
                    "xml" => sp.GetRequiredService<XmlStatementGenerator>() as IStatementGenerator,
                    _ => throw new KeyNotFoundException($"O gerador para o tipo '{key}' não foi encontrado.")
                };
            });

            builder.Services.AddScoped<StatementProcessingService>(sp =>
            {
                var outputDirectories = sp.GetRequiredService<IConfiguration>()
                                          .GetSection("OutputDirectories")
                                          .Get<OutputDirectories>();

                var statementGeneratorFunc = sp.GetRequiredService<Func<string, IStatementGenerator>>();

                return new StatementProcessingService(
                    statementGeneratorFunc("xml"),
                    outputDirectories.XmlOutputDirectory,
                    sp.GetRequiredService<ILogger<StatementProcessingService>>()
                );
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API dos Jogadores Teatrais V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}