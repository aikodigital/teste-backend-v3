using TheatricalPlayersRefactoringKata.Enums;

namespace TheatricalPlayersRefactoringKata
{
    public class Play
    {
        public string Name { get; }
        public int Lines { get; }
        public TheatricalType Type { get; }

        public Play(string name, int lines, TheatricalType type)
        {
            Name = name;
            Lines = lines;
            Type = type;
        }
    }
}