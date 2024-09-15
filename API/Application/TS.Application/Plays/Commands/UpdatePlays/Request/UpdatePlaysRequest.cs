using MediatR;
using TS.Domain.Enums;

namespace TS.Application.Plays.Commands.UpdatePlays.Request
{
    public class UpdatePlaysRequest : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ETypePLay Type { get; set; }
        public int Lines { get; set; }
    }
}