namespace TheatricalPlayersRefactoringKata.Domain
{
    public abstract class PlayTemplate
    {
        public string Name { get; private set; }
        public string Type { get; private set; }

        protected PlayTemplate(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public abstract decimal CalculateAmount(int audience, int lines);
        public abstract int CalculateVolumeCredits(int audience);
    }
}
