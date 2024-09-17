using MediatR;

namespace TS.Application.Plays.Commands.DeletePlays.Request
{
    public class DeletePlaysRequest : IRequest
    {
        public long Id { get; set; }
    }
}