using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class PlayType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int _playTypeId { get; set; }
        private string _name { get; set; }

        public int PlayTypeId { get => _playTypeId; set => _playTypeId = value; }
        public string Name { get => _name; set => _name = value; }

        public PlayType(int playTypeId, string name)
        {
            this._playTypeId = playTypeId;
            this._name = name;
        }        

        public override string ToString()
        {
            return this._name;
        }
    }
}
