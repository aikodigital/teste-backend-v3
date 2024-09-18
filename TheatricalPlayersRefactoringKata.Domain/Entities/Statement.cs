using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Domain.Validation;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Statement : Entity
    {
        private List<StatementItem> _items { get; set; }
        private string _customer { get; set; }
        private decimal _amoutOwed { get; set; }
        private int _earnedCredits { get; set; }

        [XmlElement("Customer")]
        public string Customer { get => _customer; set => _customer = value; }

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<StatementItem> Items { get => _items; set => _items = value; }

        [XmlElement("AmountOwed")]
        public decimal AmountOwed { get => _amoutOwed; set => _amoutOwed = value; }

        [XmlElement("EarnedCredits")]
        public int EarnedCredits { get => _earnedCredits; set => _earnedCredits = value; }

        public Statement() { }

        private Statement(string customer, List<StatementItem> items)
        {
            ValidateDomain(customer);
            _items = items;
            _amoutOwed = _items.Sum(x => x.AmountOwed);
            _earnedCredits = _items.Sum(x => x.EarnedCredits);
        }

        public static Statement Create(string customer, List<StatementItem> items)
        {
            return new Statement(customer, items);
        }

        private void ValidateDomain(string customer)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(customer), "Invalid customer. Customer is required.");
            _customer = customer;
        }
    }
}
