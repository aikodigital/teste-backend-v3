using System.Net;
using TheatricalPlayersRefactoringKata.Application.DTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs.PerformanceDTOs;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repositories;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IPerformanceRepository _performanceRepository;
        private readonly IPlayRepository _playRepository;

        public PerformanceService(IPerformanceRepository performanceRepository, IPlayRepository playRepository)
        {
            _performanceRepository = performanceRepository;
            _playRepository = playRepository;
        }

        public async Task<ServiceResponse<PerformanceResponse>> CreatePerformance(PerformanceRequest performanceRequest)
        {
            var response = new ServiceResponse<PerformanceResponse>();

            var perf = new Performance()
            {
                PlayId = performanceRequest.PlayId,
                Audience = performanceRequest.Audience
            };

            var play = await _playRepository.GetPlayById(performanceRequest.PlayId);

            if (play == null)
            {
                response.Data = null;
                response.Message = "Play not found";
                response.Status = HttpStatusCode.NotFound;
                return response;
            }

            perf.Credits = perf.CalculateVolumeCredits(play.Genre);

            await _performanceRepository.CreatePerformance(perf);

            var perfResponse = new PerformanceResponse(
                perf.Id,
                perf.PlayId,
                perf.Audience,
                perf.Credits);

            response.Data = perfResponse;
            response.Message = "Performance created successfully";
            response.Status = HttpStatusCode.OK;

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<PerformanceResponse>>> GetPerformances()
        {
            var response = new ServiceResponse<IEnumerable<PerformanceResponse>>();

            var perfs = await _performanceRepository.GetPerformances();

            var perfResponse = perfs.Select(p => new PerformanceResponse(
                p.Id,
                p.PlayId,
            p.Audience,
                p.Credits));

            response.Data = perfResponse;
            response.Status = HttpStatusCode.OK;

            return response;
        }
    }
}
