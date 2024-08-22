using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Data
{
    public class Play
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int Lines { get; set; }

        public List<Performance> Performances { get; set; } = new List<Performance>();
    }
}
