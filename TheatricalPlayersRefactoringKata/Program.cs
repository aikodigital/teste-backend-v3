using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Services;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Converters;
using TheatricalPlayersRefactoringKata.Infrastructure.Services;
using TheatricalPlayersRefactoringKata.Application.Factories;
using TheatricalPlayersRefactoringKata.Infrastructure;
using System.Linq;
using TheatricalPlayersRefactoringKata.Core.Entities;

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
                   options.JsonSerializerOptions.Converters.Add(new GenreConverter());
                   options.JsonSerializerOptions.Converters.Add(new PlayConverter());
               });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API dos Jogadores Teatrais", Version = "v1" });
            });

            builder.Services.AddTransient<TragedyCalculator>();
            builder.Services.AddTransient<ComedyCalculator>();
            builder.Services.AddTransient<HistoricalCalculator>();

            builder.Services.AddSingleton<Dictionary<string, IPerformanceCalculator>>(serviceProvider =>
            {
                var calculators = serviceProvider.GetServices<IPerformanceCalculator>();
                return calculators.ToDictionary(c => c.GetType().Name.Replace("Calculator", ""), c => c);
            });

            builder.Services.AddTransient<Func<string, IStatementGenerator>>(serviceProvider => key =>
            {
                if (key == "xml")
                {
                    return new XmlStatementGenerator(serviceProvider.GetServices<IPerformanceCalculator>());
                }
                throw new KeyNotFoundException($"O gerador para o tipo '{key}' não foi encontrado.");
            });

            builder.Services.AddTransient<TextStatementGenerator>();
            builder.Services.AddTransient<XmlStatementGenerator>();

            builder.Services.AddTransient<PerformanceFactory>();

            builder.Services.AddSingleton<Dictionary<string, Play>>();

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