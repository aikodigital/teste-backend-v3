﻿using Main.Application.Services.StatementPrinter;
using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Tests;
using Xunit;
using Xunit.DependencyInjection;
using Xunit;
using Xunit.DependencyInjection;
using Main.Domain.Services;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IStatementPrinterService, StatementPrinterService>();
            services.AddTransient<StatementFormatter>();
            services.AddTransient<StatementCalculator>();
        }
    }
}
