using MediatR;
using TS.Application.Customers.Queries.GetCustomers.Response;

namespace TS.Application.Customers.Queries.GetCustomers.Request
{
    public class GetCustomersRequest : IRequest<GetCustomersResponse>
    {
        public long Id { get; set; }
    }
}