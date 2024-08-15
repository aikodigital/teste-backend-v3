using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.Validations.Expenses.Register;
public interface IRegisterExpenseValidation
{
    Task<ResponseExpense> Execute(RequestExpenses request);
}
