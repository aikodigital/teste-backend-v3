using MediatR;
using TS.Application.Performances.Queries.GetPerformances.Request;
using TS.Application.Performances.Queries.GetPerformances.Response;
using TS.Domain.Repositories.Performances;

namespace TS.Application.Performances.Queries.GetPerformances
{
    public class GetPerformancesHandler(IPerformancesRepository performancesRepository) : IRequestHandler<GetPerformancesRequest, GetPerformancesResponse>
    {
        private readonly IPerformancesRepository _performancesRepository = performancesRepository;

        public async Task<GetPerformancesResponse> Handle(GetPerformancesRequest request, CancellationToken cancellationToken)
        {
            var performances = await _performancesRepository.GetAsync(request.Id);
            var responses = new GetPerformancesResponse
            {
                Id = performances!.Id,
                PlayId = performances.PlayId,
                Audience = performances.Audience
            };

            return responses;
        }
    }
}