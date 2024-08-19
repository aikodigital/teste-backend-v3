using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ApprovalTests;
using ApprovalTests.Reporters;
namespace TheatricalPlayersRefactoringKata.Tests;

public class RequestPlay
{

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void Validate()
    {
       var validator = new RegisterPlayValidator();
       var request =
       {
           Name: "Play test",
           Lines:2032,
           Type: 3
       }
        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();

    Approvals.Verify(result);
    }
}

