using System;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Play
{
    [Key, Required]
    public string Name { get; set; } //The initial logic assumes that each Play is identified by its name.

    [Required]
    public int Lines { get; set; }

    [Required]
    public string Type { get; set; }

    public Play() { }
    public Play(string name, int lines, string type)
    {
        Name = name;
        Lines = lines;
        Type = type;
    }
}
