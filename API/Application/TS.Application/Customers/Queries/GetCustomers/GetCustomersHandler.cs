using MediatR;
using TS.Application.Customers.Queries.GetCustomers.Request;
using TS.Application.Customers.Queries.GetCustomers.Response;
using TS.Domain.Repositories.Customers;

namespace TS.Application.Customers.Queries.GetCustomers
{
    public class GetCustomersHandler(ICustomersRepository customersRepository) : IRequestHandler<GetCustomersRequest, GetCustomersResponse>
    {
        private readonly ICustomersRepository _customersRepository = customersRepository;

        public async Task<GetCustomersResponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        {
            var customers = await _customersRepository.GetAsync(request.Id);
            var responses = new GetCustomersResponse
            {
                Id = customers!.Id,
                Name = customers.Name,
                Age = customers.Age,
                LoyaltyCredit = customers.LoyaltyCredit
            };

            return responses;
        }
    }
}