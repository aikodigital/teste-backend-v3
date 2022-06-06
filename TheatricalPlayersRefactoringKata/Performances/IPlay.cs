namespace TheatricalPlayersRefactoringKata.Performances
{
    public interface IPlay
    {
        decimal BaseValue { get; }
        string Name { get; }

        void CalculateBaseValue(int audience);

        decimal GetLines();
        
        int GetCredits(int audience);
    }
}
