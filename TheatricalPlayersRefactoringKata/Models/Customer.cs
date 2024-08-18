namespace TheatricalPlayersRefactoringKata.Models
{
    public class Customer
    {
        private string _name;
        private int _credits;

        public string Name { get => _name; set => _name = value; }
        public int Credits { get => _credits; set => _credits = value; }

        public Customer(string name, int credits)
        {
            _name = name;
            _credits = credits;
        }
    }
}
