using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Models;

public class PlayModel
{
    private string _id;
    private string _name;
    private int _lines;
    private TypePlay _type;

    public string Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public TypePlay Type { get => _type; set => _type = value; }

    public PlayModel(string name, int lines, TypePlay type)
    {
        _name = name;
        _lines = lines;
        _type = type;
    }

    public static PlayModel ConvertToModel(Play play)
    {
        return new PlayModel(play.Name, play.Lines, (TypePlay)play.Type)
        {
            Id = play.Id
        };
    }

    public static List<PlayModel> ConvertToModels(IEnumerable<Play> play)
    {
        return play.ToList().ConvertAll(playModel =>
            new PlayModel(playModel.Name, playModel.Lines, (TypePlay)playModel.Type)
            {
                Id = playModel.Id
            });
    }

    public static Play ConvertToEntity(PlayModel playModel)
    {
        return new Play
        {
            Id = playModel.Id,
            Name = playModel.Name,
            Lines = playModel.Lines,
            Type = (int)playModel.Type
        };
    }
}

public enum TypePlay
{
    Tragedy,
    Comedy,
    History,
}
