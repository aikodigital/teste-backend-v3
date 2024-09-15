using MediatR;
using TS.Application.Customers.Commands.AddCustomers.Request;
using TS.Domain.Entities;
using TS.Domain.Repositories.Customers;

namespace TS.Application.Customers.Commands.AddCustomers
{
    public class AddCustomersHandler(ICustomersRepository customersRepository) : IRequestHandler<AddCustomersRequest>
    {
        private readonly ICustomersRepository _customersRepository = customersRepository;

        public async Task Handle(AddCustomersRequest request, CancellationToken cancellationToken)
        {
            var addTo = new Customer
            {
                Id = request.Id,
                Name = request.Name,
                Age = request.Age,
                LoyaltyCredit = request.LoyaltyCredit
            };

            await _customersRepository.CreateAsync(addTo);
        }
    }
}