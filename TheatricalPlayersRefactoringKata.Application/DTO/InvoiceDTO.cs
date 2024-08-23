using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Application.DTO
{
    public class InvoiceDTO
    {
        public string Customer { get; set ; }
        public List<PerformanceDTO> Performances { get ; set; }
    }
}
