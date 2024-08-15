using AutoMapper;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Domain.Repos;
using TheatricalPlayersRefactoringKata.Exception.ExceptionBase;

namespace TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;
public class GeInvoiceByIdValidation : IGetInvoiceByIdValidation
{
    private readonly IInvoice _repos;
    private readonly IMapper _mapper;

    public GeInvoiceByIdValidation(IInvoice repos, IMapper mapper)
    {
        _repos = repos;
        _mapper = mapper;
    }

    public async Task<ResponseInvoice> Execute(long id)
    {
        var result = await _repos.GetById(id);

        if (result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.Expense_Not_Found);
        }

        return _mapper.Map<ResponseInvoice>(result);
    }
}
