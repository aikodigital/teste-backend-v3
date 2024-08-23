namespace TheatricalPlayersRefactoringKata.API.Repositories.DTOs;

public record PerfRequest(int Audience);

public record PerfResponse(Guid Id, int Audience, int Amount, int VolumeCredits, string PlayName);