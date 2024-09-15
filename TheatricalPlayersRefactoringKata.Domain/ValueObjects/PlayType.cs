using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Extensions;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Strategies;
using TheatricalPlayersRefactoringKata.Domain.Validation;

namespace TheatricalPlayersRefactoringKata.Domain.ValueObjects
{
    public class PlayType : ValueObject<PlayType>
    {
        public string Name { get => TypeEnum.GetPlayTypeName(); }
        public PlayTypeEnum TypeEnum { get; private set; }

        public PlayType(PlayTypeEnum typeEnum)
        {
            ValidateDomain(typeEnum);
        }

        private void ValidateDomain(PlayTypeEnum typeEnum)
        {
            DomainExceptionValidation.When(!Enum.IsDefined(typeEnum), "Invalid play type. Play type must be mapped in the application.");
            TypeEnum = typeEnum;
        }

        protected override decimal GetHashCodeCore()
        {
            decimal hashCode = this.Name.GetHashCode();
            return hashCode;
        }

        protected override bool EqualsCore(PlayType other)
        {
            return other.Name == this.Name;
        }

        public IPlayTypeStrategy GetStrategies()
        {
            return TypeEnum switch
            {
                PlayTypeEnum.Comedy => new ComedyPlayTypeStrategies(),
                PlayTypeEnum.Tragedy => new TragedyPlayTypeStrategies(),
                PlayTypeEnum.History => new HistoryPlayTypeStrategies(),
                _ => throw new Exception("Strategy does not exists or is not mapped.")
            };
        }
    }
}
