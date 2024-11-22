using CrossCutting;

namespace Aplication.DTO
{
    public class PlayDto
    {
        public string Name { get; private set; }
        public int Lines { get; private set; }
        public PlayType Type { get; private set; }

        public override string ToString()
        => $"{Name} - {Lines}";

        public PlayDto(string name, int lines, PlayType type)
        {
            Name = name;
            Lines = lines;
            Type = type;
        }
    }
}
