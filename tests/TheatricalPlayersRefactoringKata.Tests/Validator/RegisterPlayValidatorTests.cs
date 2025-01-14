using FluentAssertions;
using TheatricalPlayersRefactoringKata.Application.UseCases.Plays;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Exception;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Validator
{
    public class RegisterPlayValidatorTests
    {
        [Theory]
        [InlineData("comedy")]
        [InlineData("tragedy")]
        [InlineData("history")]
        public void Success(string type)
        {
            var validator = new PlayValidator(); //initializes the class of validation
            var request = new RequestPlayJson() { Name = "As You Like It", Lines = 2670, Type = type }; //simulates the request

            var result = validator.Validate(request); //validates the datas of request

            result.IsValid.Should().BeTrue(); //verifies the results of validations
        }

        [Fact]
        public void Error_Name_Not_Empty()
        {
            var validator = new PlayValidator(); //initializes the class of validation
            var request = new RequestPlayJson() { Name = "", Lines = 2670, Type = "comedy" }; //simulates the request

            var result = validator.Validate(request); //validates the datas of request

            result.IsValid.Should().BeFalse(); //verifies the results of validations
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_NOT_EMPTY));
        }

        [Fact]
        public void Error_Lines_Must_Be_Greater_Then_Zero()
        {
            var validator = new PlayValidator(); //initializes the class of validation
            var request = new RequestPlayJson() { Name = "As You Like It", Lines = 0, Type = "comedy" }; //simulates the request

            var result = validator.Validate(request); //validates the datas of request

            result.IsValid.Should().BeFalse(); //verifies the results of validations
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.LINES_MUST_BE_GREATER_THAN_ZERO));
        }

        [Fact]
        public void Error_Invalid_Genre()
        {
            var validator = new PlayValidator(); //initializes the class of validation
            var request = new RequestPlayJson() { Name = "As You Like It", Lines = 2670, Type = "abc" }; //simulates the request

            var result = validator.Validate(request); //validates the datas of request

            result.IsValid.Should().BeFalse(); //verifies the results of validations
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_FORMAT_GENRE));
        }
    }
}
