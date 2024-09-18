using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class TheaterPlayDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IList<PlayDTO> Players { get; set; } = [];
    }
}
