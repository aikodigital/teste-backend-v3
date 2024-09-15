namespace TS.Presentation.ViewModels
{
    public class AddCustomerViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal LoyaltyCredit { get; set; }
    }
}