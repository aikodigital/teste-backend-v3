using Aplication.DTO;
using Aplication.Interfaces;
using CrossCutting;

namespace Aplication.Services
{
    public class PlayService : IPlayService
    {
        public List<PlayDto> GetPlays()
        => new()
            {
                new PlayDto("Hamlet", 4024, PlayType.tragedy),
                new PlayDto("As You Like It", 2670, PlayType.comedy),
                new PlayDto("Othello", 3560,        PlayType.tragedy),
                new PlayDto("Henry V", 3227, PlayType.history),
                new PlayDto("King John", 2648,   PlayType.history),
                new PlayDto("Richard III", 3718, PlayType.history)
            };
    }
}
