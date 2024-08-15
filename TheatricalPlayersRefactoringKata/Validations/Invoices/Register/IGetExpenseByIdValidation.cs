using CashFlow.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.Validations.Expenses.Register;
public interface IGetExpenseByIdValidation
{
    Task<ResponseExpense> Execute(long id);
}