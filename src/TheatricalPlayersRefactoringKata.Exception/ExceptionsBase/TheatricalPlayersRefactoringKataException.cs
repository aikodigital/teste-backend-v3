namespace TheatricalPlayersRefactoringKata.Exception.ExceptionsBase
{
    public abstract class TheatricalPlayersRefactoringKataException : System.Exception
    {
        public abstract int StatusCode { get; }

        protected TheatricalPlayersRefactoringKataException(string message) : base(message) { }

        public abstract List<string> GetErrors();
    }
}
