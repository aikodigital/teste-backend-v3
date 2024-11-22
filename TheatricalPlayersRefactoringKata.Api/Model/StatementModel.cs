using System.ComponentModel.DataAnnotations;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Api.Model
{
    public class StatementModel
    {
        [Required]
        public string Customer { get; set; }

        [Required]
        public List<Performance> Performances { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalCredits { get; set; }
        [Range(0, double.MaxValue)]
        public decimal TotalCost { get; set; }

        public static Statement MapTo(StatementModel model)
        {

            return new Statement()
            {
                Customer = model.Customer,
                TotalCost = model.TotalCost,
                TotalCredits = model.TotalCredits
            };
        }

        public static Domain.Entities.Performance MapTo(Performance model)
        {

            return new Domain.Entities.Performance()
            {
                PlayId = model.PlayId,
                StatementId = model.StatementId,
                Audience = model.Audience,
                Cost = model.Cost,
                Credits = model.Credits
            };
        }

        public static Domain.Entities.Play MapTo(Play model)
        {

            return new Domain.Entities.Play()
            {
                Id = model.PlayId,
                Name = model.Name,
                Lines = model.Lines,
                Type = model.Type,
            };
        }
    }
}
