using MediatR;
using TS.Application.Performances.Commands.DeletePerformances.Request;
using TS.Domain.Repositories.Performances;

namespace TS.Application.Performances.Commands.DeletePerformances
{
    public class DeletePerformancesHandler(IPerformancesRepository performancesRepository) : IRequestHandler<DeletePerformancesRequest>
    {
        private readonly IPerformancesRepository _performancesRepository = performancesRepository;

        public async Task Handle(DeletePerformancesRequest request, CancellationToken cancellationToken)
        {
            await _performancesRepository.DeleteAsync(request.Id);
        }
    }
}