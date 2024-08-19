using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Communication.Requests;

namespace TheatricalPlayersRefactoringKata.Tests;
public class RequestPlayBuilder
{
    public static RequestPlay Build()
    {
        return new Faker<RequestPlay>()
          .RuleFor(r => r.Name, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Lines, faker => faker.Commerce.Random.Number(min: 1000, max: 4000))
            .RuleFor(r => r.Type, faker => Domain.Enums.PlayTypes.comedy));
    }

}