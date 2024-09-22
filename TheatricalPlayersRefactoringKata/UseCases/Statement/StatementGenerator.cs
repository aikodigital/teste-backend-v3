using TheatricalPlayersRefactoringKata.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
namespace TheatricalPlayersRefactoringKata;

public class StatementGenerator
{    
    private static StatementInput _statementinput;
    public StatementGenerator(StatementInput statementinput)
    {
         _statementinput = statementinput;
    }

    public Statement GenerateStatement()
    {
        Statement statement= new Statement();
        List<Item> itemcollection = new List<Item>();
        statement.Customer = _statementinput.invoice.Customer;
        foreach (var perf in _statementinput.invoice.Performances)
        {
            var play =  _statementinput.plays[perf.PlayId];
            Item item = new Item();
            item.Name = play.Name;
            int line = play.Lines < 1000 ? 1000 : play.Lines > 4000 ? 4000 : play.Lines;
            item.AmountOwed = CalculateAmount(play.Type,perf.Audience,line);
            item.EarnedCredits = CalculateEarnedCredits(perf.Audience,play.Type);
            item.Seats = perf.Audience;  
            itemcollection.Add(item);      
        }
        statement.Items = itemcollection;
        statement.EarnedCredits = statement.Items.Sum(x => x.EarnedCredits);  
        statement.AmountOwed = statement.Items.Sum(x => x.AmountOwed);  

        return statement;
    }

    public int CalculateEarnedCredits(int audience , Domain.enums.PlayType type = Domain.enums.PlayType.none)
    {         
        int _volumeCredits = 0;
        _volumeCredits += Math.Max(audience - 30, 0);
        if (type == Domain.enums.PlayType.comedy ) _volumeCredits += (int)Math.Floor((decimal)audience / 5);
        return _volumeCredits;
    }

    public decimal CalculateAmount(Domain.enums.PlayType type,int audience,int lines)
    {
        int thisAmount = 0;
        switch (type)
        {
            case Domain.enums.PlayType.comedy :
            thisAmount = CalculateComedy(audience,lines);
            break;

            case Domain.enums.PlayType.tragedy :
            thisAmount  = CalculateTragedy(audience,lines);
            break;

            case Domain.enums.PlayType.history :
            thisAmount = CalculateHistory(audience,lines);
            break;
        }
    
        return Convert.ToDecimal(thisAmount)/100;
    }

    private int CalculateComedy(int audience, int lines)
    {
        int thisAmount = lines * 10;
        if (audience > 20) 
        {
            thisAmount += 10000 + 500 * (audience - 20);
        }
        thisAmount += 300 * audience;

        return thisAmount;
    }

    private int CalculateTragedy(int audience, int lines)
    {
        int thisAmount = lines * 10;
        if (audience > 30) 
        {
            thisAmount += 1000 * (audience - 30);
        }
        return thisAmount;
    }

    private int CalculateHistory(int audience, int lines)
    {
         return CalculateComedy(audience,lines) + CalculateTragedy(audience,lines);
    }
}