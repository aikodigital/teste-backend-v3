using TS.Domain.Enums;

namespace TS.Application.Plays.Queries.GetAllPlays.Response
{
    public class GetAllPlaysResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ETypePLay Type { get; set; }
        public int Lines { get; set; }
    }
}