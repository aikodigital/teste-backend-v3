using CashFlow.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.Validations.Expenses.Register;
public interface IGetAllExpenseValidation
{
    Task<ResponseExpenses> Execute();
}
