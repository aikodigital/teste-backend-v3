using MediatR;

namespace TS.Application.Performances.Commands.DeletePerformances.Request
{
    public class DeletePerformancesRequest : IRequest
    {
        public long Id { get; set; }
    }
}