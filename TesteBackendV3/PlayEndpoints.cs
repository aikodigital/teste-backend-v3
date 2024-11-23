using Aplication.Services.Interfaces;
using AutoMapper;

namespace TesteBackendV3
{
    public static class PlayEndpoints
    {
        public static void GetPlays(WebApplication app)
        {
            app.MapGet("/plays", (IPlayService playService, IMapper mapper) =>
            {
                var playsDtos = playService.GetPlays();
                return Results.Ok(playsDtos);
            }).WithName("GetPlays").WithTags("Plays");
        }
    }
}
