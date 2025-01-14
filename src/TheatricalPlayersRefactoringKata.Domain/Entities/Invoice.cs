using TheatricalPlayersRefactoringKata.Domain.ValueObjects;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Invoice
    {
        private string _customer;
        private List<Performance> _performances;

        public string Customer { get => _customer; set => _customer = value; }
        public List<Performance> Performances { get => _performances; set => _performances = value; }

        public Invoice(string customer, List<Performance> performances)
        {
            this._customer = customer;
            this._performances = performances;
        }
    }
}
