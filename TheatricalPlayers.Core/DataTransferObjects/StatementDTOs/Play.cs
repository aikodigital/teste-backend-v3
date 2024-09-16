using TheatricalPlayers.Core.Enums;

namespace TheatricalPlayers.Core.DataTransferObjects.StatementDTOs
{
    public class Play
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Lines { get; set; }
        public PlayTypeEnum Type { get; set; }
    }
}