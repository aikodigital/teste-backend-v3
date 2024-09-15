using MediatR;
using TS.Application.Customers.Queries.GetAllCustomers.Response;

namespace TS.Application.Customers.Queries.GetAllCustomers.Request
{
    public class GetAllCustomersRequest : IRequest<IEnumerable<GetAllCustomersResponse>>
    {
        public string? Term { get; set; }
    }
}