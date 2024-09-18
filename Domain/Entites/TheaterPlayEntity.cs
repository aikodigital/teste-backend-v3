using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class TheaterPlayEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IList<PlayEntity> Players { get; set; } = [];
    }
}
