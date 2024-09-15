namespace TS.Application.Customers.Queries.GetAllCustomers.Response
{
    public class GetAllCustomersResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal LoyaltyCredit { get; set; }
    }
}