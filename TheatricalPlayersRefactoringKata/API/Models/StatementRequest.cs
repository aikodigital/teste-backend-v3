using System.Collections.Generic;
using System.Text.Json.Serialization;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.API.Models
{
    public class StatementRequest
    {
        [JsonPropertyName("invoice")]
        public Invoice Invoice { get; set; }

        [JsonPropertyName("plays")]
        public Dictionary<string, Play> Plays { get; set; }
    }
}