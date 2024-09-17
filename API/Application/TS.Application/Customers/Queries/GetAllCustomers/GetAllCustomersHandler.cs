using MediatR;
using TS.Application.Customers.Queries.GetAllCustomers.Request;
using TS.Application.Customers.Queries.GetAllCustomers.Response;
using TS.Domain.Repositories.Customers;

namespace TS.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersHandler(ICustomersRepository customersRepository) : IRequestHandler<GetAllCustomersRequest, IEnumerable<GetAllCustomersResponse>>
    {
        private readonly ICustomersRepository _customersRepository = customersRepository;

        public async Task<IEnumerable<GetAllCustomersResponse>> Handle(GetAllCustomersRequest request, CancellationToken cancellationToken)
        {
            var customers = await _customersRepository.GetAllAsync();
            var responses = customers.Where(c => string.IsNullOrWhiteSpace(request.Term) ||
                                          c.Id.ToString().Trim().Contains(request.Term, StringComparison.CurrentCultureIgnoreCase)).Select(res => new GetAllCustomersResponse
                                          {
                                              Id = res.Id,
                                              Name = res.Name,
                                              Age = res.Age,
                                              LoyaltyCredit = res.LoyaltyCredit
                                          }).ToList();
            return responses;
        }
    }
}