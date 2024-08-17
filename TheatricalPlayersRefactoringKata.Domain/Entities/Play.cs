using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Play
    {
        public string Name { get; set; }
        public int Lines { get; set; }
        public Genre Genre { get; set; }

        public Play(string name, int lines, Genre Genre)
        {
            Name = name;
            Lines = lines;
            this.Genre = Genre;
        }
    }
}