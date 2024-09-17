using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Model;

public class Play
{
    public string Name { get; set; }
    public int Lines { get; set; }
    public string Type { get; set; }
    [Key]
    public int PlayId { get; set; }

    public Play()
    {
            
    }
    public Play(string name, int lines, string type, int playId = default)
    {
        Name = name;
        Lines = lines;
        Type = type;
        PlayId = playId;
    }
}
