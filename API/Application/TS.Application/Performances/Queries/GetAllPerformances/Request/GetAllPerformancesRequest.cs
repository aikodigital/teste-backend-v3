using MediatR;
using TS.Application.Performances.Queries.GetAllPerformances.Response;

namespace TS.Application.Performances.Queries.GetAllPerformances.Request
{
    public class GetAllPerformancesRequest : IRequest<IEnumerable<GetAllPerformancesResponse>>
    {
        public string? Term { get; set; }
    }
}