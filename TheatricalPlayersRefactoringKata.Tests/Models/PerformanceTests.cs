using Xunit;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class PerformanceTests
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            var playId = "hamlet";
            var audience = 55;

            var performance = new Performance(playId, audience);

            Assert.Equal(playId, performance.PlayId);
            Assert.Equal(audience, performance.Audience);
        }

        [Fact]
        public void SetPlayId_ShouldUpdatePlayIdProperty()
        {
            var performance = new Performance("hamlet", 55);

            performance.PlayId = "othello";

            Assert.Equal("othello", performance.PlayId);
        }

        [Fact]
        public void SetAudience_ShouldUpdateAudienceProperty()
        {
            var performance = new Performance("hamlet", 55);

            performance.Audience = 100;

            Assert.Equal(100, performance.Audience);
        }
    }
}