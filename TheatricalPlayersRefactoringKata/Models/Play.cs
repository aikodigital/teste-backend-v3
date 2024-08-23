using Microsoft.EntityFrameworkCore;

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

        builder.Entity<Play>()
               .HasData(new Play("Hamlet", 4024, "tragedy"),
                        new Play("As You Like It", 2670, "comedy"),
                        new Play("Othello", 3560, "tragedy"),
                        new Play("Henry V", 3227, "history"),
                        new Play("King John", 2648, "history"),
                        new Play("Richard III", 3718, "history")
                       );
    }
}
