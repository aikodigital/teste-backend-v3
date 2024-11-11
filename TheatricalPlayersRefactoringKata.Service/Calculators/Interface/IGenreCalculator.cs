namespace TheatricalPlayersRefactoringKata.Service.Calculators.Interface;

public interface IGenreCalculator
{
   uint CalculateGenre(uint audience, uint lines);
   uint CalculateCredits(uint audience);
}
