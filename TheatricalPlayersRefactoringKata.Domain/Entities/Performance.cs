using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Performance
    {

        public Performance() { }

        public Performance(string playID, int audience)
        {
            PlayId = playID;
            Audience = audience;
        }
        public int Id { get; set; }
        public string PlayId { get; set; }
        public int StatementId { get; set; }
        public int Audience { get; set; }
        public decimal Cost { get; set; }
        public int Credits { get; set; }

        public Play Play { get; set; }
        public Statement Statement { get; set; }
    }
}
