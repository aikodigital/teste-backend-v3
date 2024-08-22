namespace TheatricalPlayersRefactoringKata.Models
{
    public class Play
    {
        public string Name { get; set; }
        public int Lines { get; set; }
        public string Type { get; set; }

        public Play() { }

        public Play(string name, int lines, string type)
        {
            Name = name;
            Lines = lines;
            Type = type;
        }
    }
}
