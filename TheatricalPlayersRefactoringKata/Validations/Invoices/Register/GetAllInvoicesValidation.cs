using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repos;

namespace TheatricalPlayersRefactoringKata.Validations.Invoices.Register;

public class GetAllInvoicesValidation : IGetAllInvoiceValidation
{
    private readonly IInvoice _repo;
    private readonly IMapper _mapper;
    public GetAllInvoicesValidation(Invoice repo, IMapper mapper)
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
