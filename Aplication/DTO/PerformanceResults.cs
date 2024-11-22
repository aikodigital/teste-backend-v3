using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTO
{
    public class PerformanceResult
    {
        public string PlayName { get; set; } = string.Empty;
        public int Audience { get; set; }
        public int ValorPorPerformance { get; set; }
    }
}
