using MediatR;
using TS.Application.Performances.Queries.GetPerformances.Response;

namespace TS.Application.Performances.Queries.GetPerformances.Request
{
    public class GetPerformancesRequest : IRequest<GetPerformancesResponse>
    {
        public long Id { get; set; }
    }
}