using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Models;

public class Play
{
    private string _name;
    private int _lines;
    private string _type;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public string Type { get => _type; set => _type = value; }

    public Play(string name, int lines, string type)
    {
        _name = name;
        _lines = lines;
        _type = type;
    }

    public static void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Play>().HasKey(x => x.Name);

        builder.Entity<Play>()
               .HasOne<PlayType>()
               .WithMany()
               .HasForeignKey(e => e.Type)
               .IsRequired();
    }
}
