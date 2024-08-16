using AutoMapper;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Domain.Repos;

namespace TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;

public class GetAllInvoicesValidation : IGetAllInvoicesValidation
{
    private readonly IInvoice _repo;
    private readonly IMapper _mapper;
    public GetAllInvoicesValidation(IInvoice repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<ResponseInvoices> Execute()
    {
        var result = await _repo.GetAll();

        return new ResponseInvoices
        {
            Invoices = _mapper.Map<List<ResponseInvoice>>(result)
        };
    }
}
