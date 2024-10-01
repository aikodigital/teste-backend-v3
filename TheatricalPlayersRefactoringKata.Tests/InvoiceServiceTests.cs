using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Dtos;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;
using TheatricalPlayersRefactoringKata.Service.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class InvoiceServiceTests
{
    private readonly Mock<IInvoiceRepository> _invoiceRepository;
    private readonly Mock<IPlayRepository> _iPlayRepository;
    private readonly Mock<ITypeGenreRepository> _iTypeGenreRepository;
    private readonly Mock<ICustomerStatementRepository> _iCustomerStatementRepository;
    private readonly Mock<IStatementPrinterService> _iStatementPrinterService;

    private readonly InvoiceService _invoiceService;

    public InvoiceServiceTests()
    {
        _invoiceRepository = new Mock<IInvoiceRepository>();
        _iPlayRepository = new Mock<IPlayRepository>();
        _iTypeGenreRepository = new Mock<ITypeGenreRepository>();
        _iCustomerStatementRepository = new Mock<ICustomerStatementRepository>();
        _iStatementPrinterService = new Mock<IStatementPrinterService>();

        _invoiceService = new InvoiceService(_invoiceRepository.Object, _iPlayRepository.Object, _iStatementPrinterService.Object);
    }

    [Fact]
    public async Task AddInvoiceWithReturnTrue()
    {
        var invoiceDto = new InvoiceDto
        {
            Customer = "Teste True",
            Performances = new List<PerformanceDto>
            {
                new() { Audience = 50, PlayId = 1 },
                new() { Audience = 30, PlayId = 2 }
            }
        };

        var invoiceDb = new Domain.Entities.Invoice
        {
            Id = 1,
            Customer = invoiceDto.Customer,
            Performances = new List<Domain.Entities.Performance>
            {
                new() { Audience = 50, PlayId = 1 },
                new() { Audience = 30, PlayId = 2 }
            }
        };

        _invoiceRepository
            .Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.Invoice>()))
            .Callback<Domain.Entities.Invoice>(invoice => invoice.Id = 1) 
            .Returns(Task.CompletedTask);

        _iStatementPrinterService
            .Setup(service => service.AddStatementCustomer(It.IsAny<Domain.Entities.Invoice>()))
            .Returns(Task.CompletedTask);


        var result = await _invoiceService.Add(invoiceDto);

        Assert.True(result);
        _invoiceRepository.Verify(repo => repo.AddAsync(It.IsAny<Domain.Entities.Invoice>()), Times.Once); 
    }

    [Fact]
    public async Task AddInvoiceWithReturnFalse()
    {
        var invoiceDto = new InvoiceDto
        {
            Customer = "Teste False",
            Performances = new List<PerformanceDto>
            {
                new() { Audience = 40, PlayId = 3 },
                new() { Audience = 20, PlayId = 4 }
            }
        };

        _invoiceRepository
            .Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.Invoice>()))
            .Callback<Domain.Entities.Invoice>(invoice => invoice.Id = 0)
            .Returns(Task.CompletedTask);

        _iStatementPrinterService
            .Setup(service => service.AddStatementCustomer(It.IsAny<Domain.Entities.Invoice>()))
            .Returns(Task.CompletedTask);


        var result = await _invoiceService.Add(invoiceDto);

        Assert.False(result);
        _invoiceRepository.Verify(repo => repo.AddAsync(It.IsAny<Domain.Entities.Invoice>()), Times.Once);
    }
}
