using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Domain.Validation;

namespace TheatricalPlayersRefactoringKata.Domain.ValueObjects
{
    public class StatementItem : ValueObject<StatementItem>
    {
        private decimal _amountOwed;
        private int _earnedCredits;
        private int _seats;

        [XmlElement("AmountOwed")]
        public decimal AmountOwed { get => _amountOwed; set => _amountOwed = value; }

        [XmlElement("EarnedCredits")]
        public int EarnedCredits { get => _earnedCredits; set => _earnedCredits = value; }

        [XmlElement("Seats")]
        public int Seats { get => _seats; set => _seats = value; }

        public StatementItem() { }

        private StatementItem(decimal amountOwed, int earnedCredits, int seats)
        {
            ValidateDomain(amountOwed, earnedCredits, seats);
        }

        public static StatementItem Create(decimal amountOwed, int earnedCredits, int seats)
        {
            return new StatementItem(amountOwed, earnedCredits, seats);
        }

        private void ValidateDomain(decimal amountOwed, int earnedCredits, int seats)
        {
            DomainExceptionValidation.When(amountOwed < 0, "Invalid amount owed value. Amount owed value must be higher or equal than 0.");
            _amountOwed = amountOwed;

            DomainExceptionValidation.When(earnedCredits < 0, "Invalid earned credits value. Earned credits value must be higher or equal than 0.");
            _earnedCredits = earnedCredits;

            DomainExceptionValidation.When(seats < 0, "Invalid seats value. Seats value must be higher or equal than 0.");
            _seats = seats;
        }

        protected override bool EqualsCore(StatementItem other)
        {
            return other.AmountOwed == _amountOwed && other.EarnedCredits == _earnedCredits && other.Seats == _seats;
        }

        protected override decimal GetHashCodeCore()
        {
            return HashCode.Combine(_earnedCredits, _amountOwed, _seats);
        }
    }
}
