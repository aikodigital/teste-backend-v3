using Domain.Entities;

namespace TheatricalPlayers.WebApi.Models
{
    public class PrintRequest
    {
        public Invoice Invoice { get; set; }
        public List<Play> Plays { get; set; }
    }
}
