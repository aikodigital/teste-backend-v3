namespace TS.Application.Customers.Queries.GetCustomers.Response
{
    public class GetCustomersResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal LoyaltyCredit { get; set; }
    }
}