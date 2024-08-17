using TheatricalPlayersRefactoringKata.Communication.Requests;

namespace TheatricalPlayersRefactoringKata.Tests.CommonTestUtilites.Request;
public class RequestPlayBuilder
{
    public static RequestPlay Build()
    {
        return new RequestPlay
        {
            Name = "Teste",
            Lines = 2023,
            Type = Domain.Enums.PlayTypes.comedy,
        };
    }
}
