using AutoMapper;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Domain.Repos;
using TheatricalPlayersRefactoringKata.Exception;
using TheatricalPlayersRefactoringKata.Exception.ExceptionBase;

namespace TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;
public class GeInvoiceByCustomerValidation : IGetInvoiceByCustomerValidation
{
    private readonly IInvoice _repos;
    private readonly IMapper _mapper;

    public GeInvoiceByCustomerValidation(IInvoice repos, IMapper mapper)
    {
        _repos = repos;
        _mapper = mapper;
    }

    public async Task<ResponseInvoice> Execute(string name)
    {
        var result = await _repos.GetByCustomer(name);

        if (result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.INVOICE_NOT_FOUND);
        }

        return _mapper.Map<ResponseInvoice>(result);
    }
}
