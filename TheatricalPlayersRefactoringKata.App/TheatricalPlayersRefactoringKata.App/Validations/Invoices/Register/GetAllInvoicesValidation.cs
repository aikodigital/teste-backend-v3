using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos.Expenses;

namespace TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;

public class GetAllInvoicesValidation : IGetAllInvoiceValidation
{
    private readonly IExpenses _repo;
    private readonly IMapper _mapper;
    public GetAllInvoicesValidation(IExpenses repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<ResponseExpenses> Execute()
    {
        var result = await _repo.GetAll();

        return new ResponseExpenses
        {
            Expenses = _mapper.Map<List<ResponseShortExpense>>(result)
        };
    }
}
