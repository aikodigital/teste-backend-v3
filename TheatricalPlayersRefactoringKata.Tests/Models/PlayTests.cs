using Xunit;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Enums;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class PlayTests
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            var name = "Hamlet";
            var lines = 4024;
            var type = PlayType.Tragedy;

            var play = new Play(name, lines, type);

            Assert.Equal(name, play.Name);
            Assert.Equal(lines, play.Lines);
            Assert.Equal(type, play.Type);
        }

        [Fact]
        public void SetName_ShouldUpdateNameProperty()
        {
            var play = new Play("Hamlet", 4024, PlayType.Tragedy);

            play.Name = "Othello";

            Assert.Equal("Othello", play.Name);
        }

        [Fact]
        public void SetLines_ShouldUpdateLinesProperty()
        {
            var play = new Play("Hamlet", 4024, PlayType.Tragedy);

            play.Lines = 3000;

            Assert.Equal(3000, play.Lines);
        }

        [Fact]
        public void SetType_ShouldUpdateTypeProperty()
        {
            var play = new Play("Hamlet", 4024, PlayType.Tragedy);

            play.Type = PlayType.Comedy;

            Assert.Equal(PlayType.Comedy, play.Type);
        }
    }
}