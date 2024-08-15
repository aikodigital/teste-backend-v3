namespace TheatricalPlayersRefactoringKata.Exception.ExceptionBase;
public class ErrorOnValidation : TheatricalPlayersRefactoringKataException
{
    public List<string> Errors { get; set; }

    public ErrorOnValidation(List<string> errorMessages) : base(string.Empty)
    {
        Errors = errorMessages;
    }
}