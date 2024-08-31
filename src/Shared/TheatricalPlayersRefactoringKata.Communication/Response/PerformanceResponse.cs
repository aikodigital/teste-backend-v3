namespace TheatricalPlayersRefactoringKata.Communication.Response;

public class PerformanceResponse
{
    int Performance { get; set; }
    int Amount { get; set; }
    int VolumeCredits {  get; set; }
    string PlayName {  get; set; } = string.Empty;
}
