using Moq;
using TS.Application.Invoices.Commands.AddInvoices;
using TS.Application.Invoices.Commands.AddInvoices.Request;
using TS.Application.Services;
using TS.Domain.Entities;
using TS.Domain.Enums;
using TS.Domain.Repositories.Customers;
using TS.Domain.Repositories.Invoices;
using TS.Domain.Repositories.Plays;
using Xunit;

namespace TS.Tests.Invoices
{
    public class AddInvoicesTests
    {
        private readonly Mock<IInvoicesRepository> _invoicesRepository;
        private readonly Mock<ICustomersRepository> _customersRepository;
        private readonly Mock<IPlaysRepository> _playsRepository;
        private readonly Mock<IRabbitMQServices> _rabbitMQServices;
        private readonly AddInvoicesHandler _handler;

        public AddInvoicesTests()
        {
            _invoicesRepository = new Mock<IInvoicesRepository>();
            _customersRepository = new Mock<ICustomersRepository>();
            _playsRepository = new Mock<IPlaysRepository>();
            _rabbitMQServices = new Mock<IRabbitMQServices>();
            _handler = new AddInvoicesHandler(_invoicesRepository.Object,
                                              _customersRepository.Object,
                                              _playsRepository.Object,
                                              _rabbitMQServices.Object);
        }

        [Fact]
        public async Task CustomerNotFound_ThrowsException()
        {
            //arrange   
            var request = new AddInvoicesRequest
            {
                CustomerId = 0
            };

            _customersRepository.Setup(_ => _.GetAsync(request.CustomerId))
                                .ReturnsAsync(() => null);

            //assert & action 
            await Assert.ThrowsAnyAsync<Exception>(async () => await _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task PlayNotFound_ThrowsException()
        {
            //arrange   
            var request = new AddInvoicesRequest
            {
                CustomerId = 1
            };

            var customer = new Customer
            {
                Id = request.CustomerId,
                Name = "Henrique contoso",
                Age = 10,
                LoyaltyCredit = 0
            };

            _customersRepository.Setup(_ => _.GetAsync(request.CustomerId))
                                .ReturnsAsync(() => customer);

            _playsRepository.Setup(_ => _.GetAllAsync())
                                         .ReturnsAsync(() => null!);

            //assert & action 
            await Assert.ThrowsAnyAsync<Exception>(async () => await _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Add_New_Inovoices()
        {
            //arrange   
            var request = new AddInvoicesRequest
            {
                CustomerId = 1,
                Seats = 30,
                TypeFile = ETypeFile.TXT
            };

            var customer = new Customer
            {
                Id = request.CustomerId,
                Name = "Henrique contoso",
                Age = 10,
                LoyaltyCredit = 0
            };

            var play = new Play
            {
                Id = 1,
                Name = "Bela e a Fera",
                Lines = 3000,
                Type = (int)ETypePLay.History
            };

            request.Performances = [
                new AddInvoicePerformances
                {
                    PlayId = play.Id,
                    Audience = 40
                }
            ];

            _customersRepository.Setup(_ => _.GetAsync(request.CustomerId))
                                .ReturnsAsync(() => customer);

            _playsRepository.Setup(_ => _.GetAllAsync())
                                         .ReturnsAsync(() => [play]);

            //action 
            await _handler.Handle(request, CancellationToken.None);

            _invoicesRepository.Verify(_ => _.CreateAsync(It.IsAny<Invoice>()), Times.Once);
            _rabbitMQServices.Verify(_ => _.Publisher(It.IsAny<MessageQueue>()), Times.Once);

            //assert
            Assert.True(true);
        }
    }
}