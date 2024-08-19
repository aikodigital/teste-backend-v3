using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;
using TheatricalPlayersRefactoringKata.Core.Entities;

public class StatementRequest
{
    [JsonPropertyName("invoice")]
    public Invoice Invoice { get; set; }

    [JsonPropertyName("plays")]
    public Dictionary<Guid, Play> Plays { get; set; }
}