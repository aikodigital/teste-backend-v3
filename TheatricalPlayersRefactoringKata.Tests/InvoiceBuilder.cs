using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Tests;
public class InvoiceBuilder
{
    public static List<Expense> Collection(User user, uint count = 2)
    {
        var list = new List<Expense>();

        if (count == 0)
            count = 1;

        var expenseId = 1;

        for (int i = 0; i < count; i++)
        {
            var expense = Build(user);
            expense.Id = expenseId++;

            list.Add(expense);
        }

        return list;
    }

    public static Invoice Build()
    {
        return new Faker<Invoice>()
            .RuleFor(i => i.Id, _ => 1)
            .RuleFor(i => i.Customer, "BigCo")
            .RuleFor(i => i.Performances.Add(new Performance(1, "teste", 365)));
    }
}
