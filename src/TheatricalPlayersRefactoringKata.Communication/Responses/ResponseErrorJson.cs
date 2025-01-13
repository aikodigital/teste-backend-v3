namespace TheatricalPlayersRefactoringKata.Communication.Responses
{
    public class ResponseErrorJson
    {
        public List<string> ErrorMessages { get; set; } = new List<string>();

        public ResponseErrorJson(string errorMessage)
        {
            ErrorMessages = new List<string>() { errorMessage };
        }

        public ResponseErrorJson(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
