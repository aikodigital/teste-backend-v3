using MediatR;
using TS.Application.Customers.Commands.UpdateCustomers.Request;
using TS.Domain.Entities;
using TS.Domain.Repositories.Customers;

namespace TS.Application.Customers.Commands.UpdateCustomers
{
    public class UpdateCustomersHandler(ICustomersRepository customersRepository) : IRequestHandler<UpdateCustomersRequest>
    {
        private readonly ICustomersRepository _customersRepository = customersRepository;

        public async Task Handle(UpdateCustomersRequest request, CancellationToken cancellationToken)
        {
            var updateTo = new Customer
            {
                Id = request.Id,
                Name = request.Name,
                Age = request.Age,
                LoyaltyCredit = request.LoyaltyCredit
            };

            await _customersRepository.UpdateAsync(updateTo);
        }
    }
}