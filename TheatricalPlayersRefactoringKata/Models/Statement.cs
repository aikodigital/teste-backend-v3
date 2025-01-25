using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Models
{
    public class Statement
    {
        public string Custumer { get; set; }
        public List<Item> Items { get; set; }
    }
}
