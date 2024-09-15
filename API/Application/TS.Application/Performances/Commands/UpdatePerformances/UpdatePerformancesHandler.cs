using MediatR;
using TS.Application.Performances.Commands.UpdatePerformances.Request;
using TS.Domain.Entities;
using TS.Domain.Repositories.Performances;

namespace TS.Application.Performances.Commands.UpdatePerformances
{
    public class UpdatePerformancesHandler(IPerformancesRepository performancesRepository) : IRequestHandler<UpdatePerformancesRequest>
    {
        private readonly IPerformancesRepository _performancesRepository = performancesRepository;

        public async Task Handle(UpdatePerformancesRequest request, CancellationToken cancellationToken)
        {
            var updateTo = new Performance
            {
                Id = request.Id,
                PlayId = request.PlayId,
                Audience = request.Audience
            };

            await _performancesRepository.UpdateAsync(updateTo);
        }
    }
}