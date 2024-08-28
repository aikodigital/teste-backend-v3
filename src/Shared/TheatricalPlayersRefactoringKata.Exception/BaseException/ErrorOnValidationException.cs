namespace TheatricalPlayersRefactoringKata.Exception.BaseException;

public class ErrorOnValidationException : CustomizedException
{
    public List<string> ErrorMessage { get; set; }

    public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    {
        ErrorMessage = errorMessages;

    }
}
