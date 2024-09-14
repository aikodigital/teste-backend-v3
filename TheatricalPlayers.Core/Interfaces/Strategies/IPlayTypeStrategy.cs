namespace TheatricalPlayers.Core.Interfaces.Strategies;

public interface IPlayTypeStrategy
{
    int CalculateAmount(int lines, int audience);
    int CalculateVolumeCredits(int audience);
}