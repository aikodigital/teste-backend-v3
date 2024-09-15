using MediatR;
using TS.Application.Customers.Commands.DeleteCustomers.Request;
using TS.Domain.Repositories.Customers;

namespace TS.Application.Customers.Commands.DeleteCustomers
{
    public class DeleteCustomersHandler(ICustomersRepository customersRepository) : IRequestHandler<DeleteCustomersRequest>
    {
        private readonly ICustomersRepository _customersRepository = customersRepository;

        public async Task Handle(DeleteCustomersRequest request, CancellationToken cancellationToken)
        {
            await _customersRepository.DeleteAsync(request.Id);
        }
    }
}