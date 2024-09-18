using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Validation;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Entities
{
    public class PerformanceTests
    {

        [Fact]
        public void CreatePerformanceWithValidValues()
        {
            var play = new Play("Hamlet", 4024, PlayTypeEnum.Tragedy);
            int audience = 55;

            var newPerformance = new Performance(play, audience);

            Assert.Equal(audience, newPerformance.Audience);
            Assert.Equal(play, newPerformance.Play);
        }

        [Fact]
        public void CreatePerformanceWithInvalidAudience()
        {
            var play = new Play("Hamlet", 4024, PlayTypeEnum.Tragedy);

            var exception = Assert.Throws<DomainExceptionValidation>(() => new Performance(play, -1));

            Assert.Equal("Invalid value. Audience must be equal or higher than 0.", exception.Message);
        }
    }
}
