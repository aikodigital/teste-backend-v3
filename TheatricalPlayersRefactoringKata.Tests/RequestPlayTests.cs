using ApprovalTests;
using ApprovalTests.Reporters;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.App.Validations.Plays.Register;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Tests.CommonTestUtilites.Request;
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
