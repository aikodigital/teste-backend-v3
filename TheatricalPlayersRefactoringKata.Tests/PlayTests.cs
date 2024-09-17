using TheatricalPlayersRefactoringKata.Models;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class PlayTests
{
    [Fact]
    public void CriacaoValida()
    {
        var name = "Maria Antonieta";
        var lines = 1500;
        var genero = Genero.Tragedy;
        var play = new Play(name, lines, genero);

        Assert.Equal(name, play.Name);
        Assert.Equal(lines, play.Lines);
        Assert.Equal(genero, play.Type);
    }

    [Fact]
    public void AlteracaoDeDados()
    {
        var play = new Play("Dinastia", 1500, Genero.Comedy);
        play.Name = "Oscar III";
        play.Lines = 2100;
        play.Type = Genero.Tragedy;

        Assert.Equal("Oscar III", play.Name);
        Assert.Equal(2100, play.Lines);
        Assert.Equal(Genero.Tragedy, play.Type);
    }
}
