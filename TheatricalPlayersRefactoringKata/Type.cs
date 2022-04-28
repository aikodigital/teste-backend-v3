using System;

namespace TheatricalPlayersRefactoringKata;

public class Type
{
    private string _name;

    public string Name { get => _name; set => _name = value; }

    public Type(string name) {
        this._name = name;
    }

    public int CalcAmount(Performance perf){
        int amount = -1;
        switch (this._name) 
        {
            case "tragedy":
                if (perf.Audience > 30) {
                    amount = 1000 * (perf.Audience - 30);
                }
                break;
            case "comedy":
                amount = 300 * perf.Audience;
                if (perf.Audience > 20) {
                    amount += 10000 + 500 * (perf.Audience - 20);
                }
                break;
            default:
                throw new Exception("unknown type: " + this._name);
        }
        return amount;
    }
}