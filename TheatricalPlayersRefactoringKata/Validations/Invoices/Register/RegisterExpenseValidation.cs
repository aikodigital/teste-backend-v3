using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace TheatricalPlayersRefactoringKata.Validations.Expenses.Register;
public class RegisterExpenseValidation : IRegisterExpenseValidation
{
    private readonly IExpenses _repo;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;
    public RegisterExpenseValidation(
        IExpenses repo,
        IUnityOfWork unityOfWork,
        IMapper mapper
        )
    {
        _repo = repo;
        _unityOfWork = unityOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseExpense> Execute(RequestExpenses request)
    {
        Validate(request);

        var entity = _mapper.Map<Expense>(request);
        await _repo.Add(entity);

        await _unityOfWork.Commit();
        return _mapper.Map<ResponseExpense>(entity);
    }

    private void Validate(RequestExpenses request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidation(errorMessages);
        }
    }
}