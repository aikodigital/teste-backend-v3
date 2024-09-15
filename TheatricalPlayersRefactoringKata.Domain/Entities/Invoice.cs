using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Validation;

namespace TheatricalPlayersRefactoringKata.Domain;

public class Invoice : Entity
{
    private string _customer;
    private List<Performance> _performances;

    public string Customer { get => _customer; set => _customer = value; }
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    public Invoice(string customer, List<Performance> performance)
    {
        ValidateDomain(customer);
        this._performances = performance;
    }

    private void ValidateDomain(string customer)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(customer), "Invalid customer. Customer is required.");
        this._customer = customer;
    }

    public string PrintInvoiceStatement()
    {
        decimal totalAmount = 0;
        int volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", _customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (Performance performance in _performances)
        {
            decimal thisAmount = performance.Play.CalculateAmountValueByAudience(performance.Audience);
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", performance.Play.Name, thisAmount, performance.Audience);

            totalAmount += thisAmount;
            volumeCredits += performance.Play.CalculateCreditsByAudience(performance.Audience);
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

}
