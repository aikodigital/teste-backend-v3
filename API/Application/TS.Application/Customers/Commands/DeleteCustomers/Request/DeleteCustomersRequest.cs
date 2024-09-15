using MediatR;

namespace TS.Application.Customers.Commands.DeleteCustomers.Request
{
    public class DeleteCustomersRequest : IRequest
    {
        public long Id { get; set; }
    }
}