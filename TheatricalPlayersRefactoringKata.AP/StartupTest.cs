using TheatricalPlayersRefactoringKata.CrossCutting.IoC;

namespace TheatricalPlayersRefactoringKata.API
{
    public class StartupTest
    {
        public StartupTest()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddTheatricalPlayersIoC();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {

        }
    }
}