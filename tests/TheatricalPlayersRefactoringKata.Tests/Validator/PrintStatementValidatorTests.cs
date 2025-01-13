using FluentAssertions;
using TheatricalPlayersRefactoringKata.Application.UseCases.Statements;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Exception;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Validator
{
    public class PrintStatementValidatorTests
    {
        [Theory]
        [InlineData("txt")]
        [InlineData("xml")]
        public void Success(string formatFile)
        {
            var validator = new StatementValidator(); //initializes the class of validation
            var request = new RequestPrintStatementJson() { FormatFile = formatFile }; //simulates the request

            var result = validator.Validate(request); //validates the datas of request

            result.IsValid.Should().BeTrue(); //verifies the results of validations
        }

        [Fact]
        public void Error_Invalid_Format_File()
        {
            var validator = new StatementValidator(); //initializes the class of validation
            var request = new RequestPrintStatementJson() { FormatFile = "abc" }; //simulates the request

            var result = validator.Validate(request); //validates the datas of request

            result.IsValid.Should().BeFalse(); //verifies the results of validations
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_FORMAT_FILE));
        }
    }
}
