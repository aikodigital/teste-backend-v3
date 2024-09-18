namespace TheatricalPlayersRefactoringKata.Domain
{
    public class HistoryPlay : PlayTemplate
    {
        public HistoryPlay(string name) : base(name, "history") { }

        public override decimal CalculateAmount(int audience, int lines)
        {
            var tragedy = new TragedyPlay(this.Name);
            var comedy = new ComedyPlay(this.Name);
            return tragedy.CalculateAmount(audience, lines) + comedy.CalculateAmount(audience, lines);
        }

        public override int CalculateVolumeCredits(int audience)
        {
            return new TragedyPlay(this.Name).CalculateVolumeCredits(audience);
        }
    }
}
