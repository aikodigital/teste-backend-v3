namespace TheatricalPlayersRefactoringKata.Exception.ExceptionBase;
public abstract class TheatricalPlayersRefactoringKataException : SystemException
{

    protected TheatricalPlayersRefactoringKataException(string message) : base(message)
    {
    }
}