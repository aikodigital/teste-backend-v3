using FluentAssertions;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Infraestructure.Services.FileGenerator;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Services
{
    public class FileGeneratorTests
    {
        [Theory]
        [InlineData("txt")]
        [InlineData("xml")]
        public async Task Success(string formatFile)
        {
            var textFile = string.Format("<message>Hello world! I saved an file {0}</message>", formatFile);
            FileGenerator fileGenerator = new FileGenerator(); //initializes the class of service

            var result = await fileGenerator.FileGeneratorAsync(textFile, formatFile); //requests the service

            result.Should().BeTrue(); //verifies the results of validations
        }
    }
}
