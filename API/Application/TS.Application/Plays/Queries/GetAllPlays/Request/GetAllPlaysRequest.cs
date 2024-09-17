using MediatR;
using TS.Application.Plays.Queries.GetAllPlays.Response;

namespace TS.Application.Plays.Queries.GetAllPlays.Request
{
    public class GetAllPlaysRequest : IRequest<IEnumerable<GetAllPlaysResponse>>
    {
        public string? Term { get; set; }
    }
}