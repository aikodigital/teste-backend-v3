using Swashbuckle.AspNetCore.Filters;
using TheatricalPlayersRefactoringWebAPI.DTO;

namespace TheatricalPlayersRefactoringWebAPI
{
    public class StatementRequestExample : IExamplesProvider<StatementRequest>
    {
        // Exemplo padrão de peças para exibição no swagger
        public StatementRequest GetExamples()
        {
            return new StatementRequest
            {
                Invoice = new InvoiceRequest
                {
                    Customer = "BigCo",
                    Performances = new List<PerformanceRequest>
                {
                    new PerformanceRequest { PlayId = "hamlet", Audience = 55 },
                    new PerformanceRequest { PlayId = "as-like", Audience = 35 },
                    new PerformanceRequest { PlayId = "othello", Audience = 40 },
                    new PerformanceRequest { PlayId = "henry-v", Audience = 20 },
                    new PerformanceRequest { PlayId = "john", Audience = 39 },
                    new PerformanceRequest { PlayId = "henry-v", Audience = 20 }
                }
                },
                Plays = new List<PlayRequest>
            {
                new PlayRequest { PlayId = "hamlet", Name = "Hamlet", Lines = 4024, Type = "tragedy" },
                new PlayRequest { PlayId = "as-like", Name = "As You Like It", Lines = 2670, Type = "comedy" },
                new PlayRequest { PlayId = "othello", Name = "Othello", Lines = 3560, Type = "tragedy" },
                new PlayRequest { PlayId = "henry-v", Name = "Henry V", Lines = 3227, Type = "history" },
                new PlayRequest { PlayId = "john", Name = "King John", Lines = 2648, Type = "history" },
                new PlayRequest { PlayId = "richard-iii", Name = "Richard III", Lines = 3718, Type = "history" }
            }
            };
        }
    }
}
