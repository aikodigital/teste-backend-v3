using System.Net;
using TheatricalPlayersRefactoringKata.Application.DTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs.PlayDTOs;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Repositories;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class PlayService : IPlayService
    {
        private readonly IPlayRepository _playRepository;

        public PlayService(IPlayRepository playRepository)
        {
            _playRepository = playRepository;
        }

        public async Task<ServiceResponse<PlayResponse>> CreatePlay(PlayRequest playRequest)
        {
            var response = new ServiceResponse<PlayResponse>();

            if (!Enum.IsDefined(typeof(Genre), playRequest.Genre))
            {
                response.Message = "Invalid genre specified.";
                response.Status = HttpStatusCode.BadRequest;
                return response;
            }

            var play = new Play()
            {
                Name = playRequest.Name,
                Lines = playRequest.Lines,
                Genre = playRequest.Genre
            };

            await _playRepository.CreatePlay(play);

            var playResponse = new PlayResponse(
               play.Id,
               play.Name,
               play.Lines,
               play.Genre);

            response.Data = playResponse;
            response.Message = "Play created successfully";
            response.Status = HttpStatusCode.OK;

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<PlayResponse>>> GetPlays()
        {
            var response = new ServiceResponse<IEnumerable<PlayResponse>>();

            var play = await _playRepository.GetPlays();

            var playResponse = play.Select(p => new PlayResponse(
                p.Id,
                p.Name,
                p.Lines,
                p.Genre));

            response.Data = playResponse;
            response.Status = HttpStatusCode.OK;

            return response;
        }
    }
}
