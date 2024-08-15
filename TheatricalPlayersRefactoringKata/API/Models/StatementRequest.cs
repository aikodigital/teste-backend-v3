using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.API.Models
{
    public class StatementRequest
    {
        public Invoice Invoice { get; set; }
        public Dictionary<string, Play> Plays { get; set; }
    }
}