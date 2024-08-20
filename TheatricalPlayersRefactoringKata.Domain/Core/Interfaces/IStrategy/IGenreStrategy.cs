using TheatricalPlayersRefactoringKata;

public interface IGenreStrategy {

    int CalculatePlayCredits(Performance perf);
    double CalculatePlayAmount(Performance perf);
}