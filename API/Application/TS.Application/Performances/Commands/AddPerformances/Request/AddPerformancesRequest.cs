using MediatR;

namespace TS.Application.Performances.Commands.AddPerformances.Request
{
    public class AddPerformancesRequest : IRequest
    {
        public long Id { get; set; }
        public long PlayId { get; set; }
        public int Audience { get; set; }
    }
}