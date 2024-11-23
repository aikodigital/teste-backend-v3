using CrossCutting;

namespace Aplication.DTO
{
    public class PlayDto
    {
        public PlayDto() { }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Lines { get; set; }
        public PlayType Type { get; set; }

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
