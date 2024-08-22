using TheatricalPlayersRefactoringKata.Application.DTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs.PlayDTOs;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IPlayService
    {
        Task<ServiceResponse<PlayResponse>> CreatePlay(PlayRequest playRequest);
        Task<ServiceResponse<IEnumerable<PlayResponse>>> GetPlays();
    }
}
