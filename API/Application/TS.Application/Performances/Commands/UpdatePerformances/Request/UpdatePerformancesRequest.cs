using MediatR;

namespace TS.Application.Performances.Commands.UpdatePerformances.Request
{
    public class UpdatePerformancesRequest : IRequest
    {
        public long Id { get; set; }
        public long PlayId { get; set; }
        public int Audience { get; set; }
    }
}