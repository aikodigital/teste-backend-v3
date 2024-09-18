using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ReportCreditEntity
    {
        public int Id { get; set; }
        public decimal AmountTotal { get; set; }
        public int Credits
        {
            get; set;
        }
    }
}
