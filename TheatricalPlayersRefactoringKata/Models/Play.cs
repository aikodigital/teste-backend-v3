using System;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Models;

public class Play
{
    [Key]
    public string Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public int Lines { get; set; }

    [Required]
    [MaxLength(50)]
    public string Type { get; set; }

    public Play()
    {
        Id = Guid.NewGuid().ToString();
    }

    public Play(string name, int lines, string type)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Lines = lines;
        Type = type;
    }
}
