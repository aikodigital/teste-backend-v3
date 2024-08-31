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
            var tragedy = new Tragedy(Name, Lines);
            var comedy = new Comedy(Name, Lines);
            return tragedy.CalculateAmount(audience) + comedy.CalculateAmount(audience);
        }

        public int CalculateVolumeCredits(int audience)
        {
            var tragedy = new Tragedy(Name, Lines);
            var comedy = new Comedy(Name, Lines);
            return tragedy.CalculateVolumeCredits(audience) + comedy.CalculateVolumeCredits(audience);
        }
    }
}
