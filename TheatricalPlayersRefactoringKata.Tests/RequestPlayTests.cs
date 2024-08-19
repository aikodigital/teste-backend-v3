using ApprovalTests;
using ApprovalTests.Reporters;
using CommonUtilites.Requests;
using FluentValidation;
using TheatricalPlayersRefactoringKata.App.Validations.Plays.Register;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;
public class RequestPlayTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void Validar()
    {
        var validator = new RegisterPlayValidator();
        var request = new RequestPlayBuilder.Build();
        var result = validator.Validate(request);

        Approvals.Verify(result);
    }
}
