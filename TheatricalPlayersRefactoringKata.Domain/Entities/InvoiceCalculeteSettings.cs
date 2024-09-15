using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class InvoiceCalculeteSettings
    {
        private Guid _id;
        private GenderType _gender;
        private int _minimumAudience;
        private decimal _bonus;
        private decimal _perAudienceAdditional;
        private decimal _perAudience;

        public InvoiceCalculeteSettings(Guid id, GenderType gender, int minimumAudience, decimal bonus, decimal perAudienceAdditional, decimal perAudience)
        {
            Id = id;
            Gender = gender;
            MinimumAudience = minimumAudience;
            Bonus = bonus;
            PerAudienceAdditional = perAudienceAdditional;
            PerAudience = perAudience;
        }

        public Guid Id { get => _id; set => _id = value; }
        public GenderType Gender { get => _gender; set => _gender = value; }
        public int MinimumAudience { get => _minimumAudience; set => _minimumAudience = value; }
        public decimal Bonus { get => _bonus; set => _bonus = value; }
        public decimal PerAudienceAdditional { get => _perAudienceAdditional; set => _perAudienceAdditional = value; }
        public decimal PerAudience { get => _perAudience; set => _perAudience = value; }
    }
}
