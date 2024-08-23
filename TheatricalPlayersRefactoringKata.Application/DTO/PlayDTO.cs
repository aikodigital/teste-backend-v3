using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata.Application.DTO
{
    public class PlayDTO
    {
        public string Name { get; set; }
        public int Lines { get; set; }
        public Enum Type { get; set; }

    }
}
