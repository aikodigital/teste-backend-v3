using MediatR;
using TS.Application.Performances.Queries.GetAllPerformances.Request;
using TS.Application.Performances.Queries.GetAllPerformances.Response;
using TS.Domain.Repositories.Performances;

namespace TS.Application.Performances.Queries.GetAllPerformances
{
    public class GetAllPerformancesHandler(IPerformancesRepository performancesRepository) : IRequestHandler<GetAllPerformancesRequest, IEnumerable<GetAllPerformancesResponse>>
    {
        private readonly IPerformancesRepository _performancesRepository = performancesRepository;

        public async Task<IEnumerable<GetAllPerformancesResponse>> Handle(GetAllPerformancesRequest request, CancellationToken cancellationToken)
        {
            var performances = await _performancesRepository.GetAllAsync();
            var responses = performances.Where(c => string.IsNullOrWhiteSpace(request.Term) ||
                                          c.Id.ToString().Trim().Contains(request.Term, StringComparison.CurrentCultureIgnoreCase)).Select(res => new GetAllPerformancesResponse
                                          {
                                              Id = res.Id,
                                              PlayId = res.PlayId,
                                              Audience = res.Audience
                                          }).ToList();
            return responses;
        }
    }
}