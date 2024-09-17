using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class InvoiceCalculeteSettings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int _id;
        private int _playTypeId;
        private int _minimumAudience;
        private decimal _bonus;
        private decimal _perAudienceAdditional;
        private decimal _perAudience;

        public InvoiceCalculeteSettings() { }
        public InvoiceCalculeteSettings(int id, int playTypeId, int minimumAudience, decimal bonus, decimal perAudienceAdditional, decimal perAudience)
        {
            _id = id;
            _playTypeId = playTypeId;
            _minimumAudience = minimumAudience;
            _bonus = bonus;
            _perAudienceAdditional = perAudienceAdditional;
            _perAudience = perAudience;
        }

        public int Id { get => _id; set => _id = value; }
        public int PlayTypeId { get => _playTypeId; set => _playTypeId = value; }
        public int MinimumAudience { get => _minimumAudience; set => _minimumAudience = value; }
        public decimal Bonus { get => _bonus; set => _bonus = value; }
        public decimal PerAudienceAdditional { get => _perAudienceAdditional; set => _perAudienceAdditional = value; }
        public decimal PerAudience { get => _perAudience; set => _perAudience = value; }
    }
}
