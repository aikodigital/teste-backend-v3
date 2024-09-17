using MediatR;

namespace TS.Application.Customers.Commands.UpdateCustomers.Request
{
    public class UpdateCustomersRequest : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal LoyaltyCredit { get; set; }
    }
}