using System;

namespace TP.Domain.Entities;

public class Play
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Lines { get; set; }
    public string Type { get; set; }

    public Play(string name, int lines, string type)
    {
        Name = name;
        Lines = lines;
        Type = type;
    }
}