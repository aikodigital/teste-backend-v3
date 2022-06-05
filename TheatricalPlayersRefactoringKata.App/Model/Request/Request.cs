namespace TheatricalPlayersRefactoringKata.App.Model.Request
{
    public class Request
    {
    }

    public class Request<T> : Request
    {
        public T Value { get; set; }
    }
}