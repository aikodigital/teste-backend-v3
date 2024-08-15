using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace TheatricalPlayersRefactoringKata.Validations.Expenses.Register;
public class GeExpenseByIdValidation : IGetExpenseByIdValidation
{
    private readonly IExpenses _repos;
    private readonly IMapper _mapper;

    public GeExpenseByIdValidation(IExpenses repos, IMapper mapper)
    {
        _repos = repos;
        _mapper = mapper;
    }

    public async Task<ResponseExpense> Execute(long id)
    {
        var result = await _repos.GetById(id);

        if (result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.Expense_Not_Found);
        }

        return _mapper.Map<ResponseExpense>(result);
    }
}
