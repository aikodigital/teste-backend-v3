using MediatR;
using TS.Application.Performances.Commands.AddPerformances.Request;
using TS.Domain.Entities;
using TS.Domain.Repositories.Performances;
using TS.Domain.Repositories.Plays;

namespace TS.Application.Performances.Commands.AddPerformances
{
    public class AddPerformancesHandler(IPerformancesRepository performancesRepository, IPlaysRepository playsRepository) : IRequestHandler<AddPerformancesRequest>
    {
        private readonly IPerformancesRepository _performancesRepository = performancesRepository;
        private readonly IPlaysRepository _playsRepository = playsRepository;

        public async Task Handle(AddPerformancesRequest request, CancellationToken cancellationToken)
        {
            var play = await _playsRepository.GetAsync(request.PlayId);

            if (play != null)
            {
                var addTo = new Performance
                {
                    Id = request.Id,
                    PlayId = request.PlayId,
                    Audience = request.Audience,
                    Play = play,
                };

                await _performancesRepository.CreateAsync(addTo);
            }
        }
    }
}