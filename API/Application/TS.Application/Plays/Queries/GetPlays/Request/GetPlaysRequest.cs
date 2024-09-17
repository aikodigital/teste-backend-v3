using MediatR;
using TS.Application.Plays.Queries.GetPlays.Response;

namespace TS.Application.Plays.Queries.GetPlays.Request
{
    public class GetPlaysRequest : IRequest<GetPlaysResponse>
    {
        public long Id { get; set; }
    }
}