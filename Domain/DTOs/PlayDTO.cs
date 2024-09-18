using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class PlayDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Lines { get; set; }
        public string Type { get; set; } = string.Empty;
        public TheaterPlayDTO TheaterPlay { get; set; }
    }
}
