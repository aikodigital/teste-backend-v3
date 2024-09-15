using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Extensions;
using TheatricalPlayersRefactoringKata.Domain.Validation;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Entities
{
    public class PlayTests
    {
        [Fact]
        public void CreatePlayEntityWithValidValues()
        {
            string name = "Othello";
            int lines = 3560;
            PlayTypeEnum type = PlayTypeEnum.Tragedy;

            Play newPlay = new Play(name, lines, type);

            Assert.Equal(name, newPlay.Name);
            Assert.Equal(lines, newPlay.Lines);
            Assert.Equal(type, newPlay.PlayType.TypeEnum);
            Assert.Equal(type.GetPlayTypeName(), newPlay.TypeName);
        }

        [Fact]
        public void CreatePlayEntityWithInvalidName()
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Play("", 1, PlayTypeEnum.Tragedy));

            Assert.Equal("Invalid name. Name is required.", exception.Message);
        }

        [Fact]
        public void CreatePlayEntityWithInvalidLines()
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Play("Othello", -1, PlayTypeEnum.Tragedy));

            Assert.Equal("Invalid value. Lines must be equal or higher than 0.", exception.Message);
        }

    }
}
