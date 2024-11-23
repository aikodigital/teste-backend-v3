using System;
using System.Text.Json.Serialization;

namespace TheatricalPlayersRefactoringKata.Entity;

public class Play
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Lines { get; set; }
    public string Type { get; set; }

    [JsonIgnore]
    public Performance Performance { get; set; }
    public Guid PerformanceId { get; set; }
}
