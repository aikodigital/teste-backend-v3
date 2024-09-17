using TS.Domain.Enums;

namespace TS.Application.Plays.Queries.GetPlays.Response
{
    public class GetPlaysResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ETypePLay Type { get; set; }
        public int Lines { get; set; }
    }
}