using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Play
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Lines { get; set; }
        public Genre Genre { get; set; }

        public Play(Guid id,string name, int lines, Genre Genre)
        {
            Id = id;
            Name = name;
            Lines = lines;
            this.Genre = Genre;
        }

        public int CalculateBaseAmount()
        {
            if (Lines < 1000) Lines = 1000;
            if (Lines > 4000) Lines = 4000;
            return Lines * 10;
        }
    }
}