using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain;

public class Play
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    private int _playId;
    private string _name;
    private int _lines;
    private int _playTypeId;

    public int PlayId { get => _playId; set => _playId = value; }
    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public int PlayTypeId { get => _playTypeId; set => _playTypeId = value; }    

    public Play() { }
    public Play(int playId, string name, int lines, int playTypeId) {
        this._playId = playId;
        this._name = name;
        this._lines = lines;
        this._playTypeId = playTypeId;
    }
}
