using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class InvoiceCreditSettings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int _id;
        private int _playTypeId;
        private int _minimumAudience;
        private decimal _bonusCreditPerAttendees;

        public InvoiceCreditSettings(int id, int playTypeId, int minimumAudience, decimal bonusCreditPerAttendees)
        {
            _id = id;
            _playTypeId = playTypeId;
            _minimumAudience = minimumAudience;
            _bonusCreditPerAttendees = bonusCreditPerAttendees;
        }

        public int Id { get => _id; set => _id = value; }
        public int PlayTypeId { get => _playTypeId; set => _playTypeId = value; }
        public int MinimumAudience { get => _minimumAudience; set => _minimumAudience = value; }
        public decimal BonusCreditPerAttendees { get => _bonusCreditPerAttendees; set => _bonusCreditPerAttendees = value; }
    }
}
