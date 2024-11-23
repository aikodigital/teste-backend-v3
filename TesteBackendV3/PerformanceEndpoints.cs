using Aplication.Services.Interfaces;
namespace TesteBackendV3
{
    public static class PerformanceEndpoints
    {
        public static void GetPerformances(WebApplication app)
        {
            app.MapGet("/performances", (IPerformanceService performanceService) =>
            {
                var performances = performanceService.GetPerformances();
                return Results.Ok(performances);
            }).WithName("GetPerformances").WithTags("Performances");
        }
       
    }
}
