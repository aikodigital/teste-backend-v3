using TheatricalPlayers.Core.Entities;
using TheatricalPlayers.Core.Enums;

namespace TheatricalPlayersRefactoringKata.Tests.Application.Mocks;

public static class InvoiceSampleDataGenerator
{
     public static (List<Play>, Invoice) CreateSampleData()
     {
         return (CreatePlays(), CreateInvoice());
     }

    private static List<Play> CreatePlays()
    {
        return new List<Play>
        {
            new Play
            {
                Id = 1,
                Name = "Hamlet",
                Lines = 4024,
                Type = PlayTypeEnum.Tragedy
            },
            new Play
            {
                Id = 2,
                Name = "As You Like It",
                Lines = 2670,
                Type = PlayTypeEnum.Comedy
            },
            new Play
            {
                Id = 3,
                Name = "Othello",
                Lines = 3560,
                Type = PlayTypeEnum.Tragedy
            },
            new Play
            {
                Id = 4,
                Name = "Henry V",
                Lines = 3227,
                Type = PlayTypeEnum.Historical
            },
            new Play
            {
                Id = 5,
                Name = "john",
                Lines = 2648,
                Type = PlayTypeEnum.Historical
            },
            new Play
            {
                Id = 6,
                Name = "Richard III",
                Lines = 3718,
                Type = PlayTypeEnum.Historical
            }
        };
    }

    private static Invoice CreateInvoice()
    {
        return new Invoice
        {
            Customer = "BigCo",
            Performances = new List<Performance>
            {
                new Performance
                {
                    PlayId = 1,
                    Audience = 55
                },
                new Performance
                {
                    PlayId = 2,
                    Audience = 35
                },
                new Performance
                {
                    PlayId = 3,
                    Audience = 40
                },
                new Performance
                {
                    PlayId = 4,
                    Audience = 20
                },
                new Performance
                {
                    PlayId = 5,
                    Audience = 39
                },
                new Performance
                {
                    PlayId = 6,
                    Audience = 20
                }
            }
        };
    }
}