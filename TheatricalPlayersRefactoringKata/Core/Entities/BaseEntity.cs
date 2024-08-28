using System;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public bool Active { get; set; } = true;    
}
