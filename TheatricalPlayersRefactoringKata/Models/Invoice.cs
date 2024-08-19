using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Models;
public class Invoice(string customer, List<Performance> performances)
{
    private string _customer = customer;
    private List<Performance> _performances = performances;

    public string Customer { get => _customer; set => _customer = value; }
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    public int TotalCharge {get => CalculateTotalCharge();}
    public int TotalCredits {get => CalculateTotalCredits();}

    private int CalculateTotalCharge() {
        int totalCharge = 0;
        foreach (Performance perf in _performances) {
            totalCharge += perf.Charge;
        }
        return totalCharge;
    }

    private int CalculateTotalCredits() {
        int totalCredits = 0;
        foreach (Performance perf in _performances) {
            totalCredits += perf.Credits;
        }
        return totalCredits; 
    }

}
