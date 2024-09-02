namespace TheatricalPlayersRefactoringKata.API.Models;

internal record CustomResponse<T>
{
    public bool Success { get; init; }
    public T Data { get; init; } = default!;
    public string Message { get; init; } = null!;
}
