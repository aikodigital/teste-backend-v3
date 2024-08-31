namespace TheatricalPlayersRefactoringKata.Models
{
    public class History : IPlay
    {
        public string Name { get; }
        public int Lines { get; }

        public History(string name, int lines)
        {
            Name = name;
            Lines = lines;
        }

        public decimal CalculateAmount(int audience)
        {
            return 30000;
        }

        public int CalculateVolumeCredits(int audience)
        {
            return 0; 
        }
    }
}
