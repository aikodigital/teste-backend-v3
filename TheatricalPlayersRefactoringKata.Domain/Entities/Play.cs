using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Play
    {
        public Play() { }
        public Play(string id, string name, int lines, string type)
        {
            Id = id;
            Name = name;
            Lines = lines;
            Type = type;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Lines { get; set; }
        public string Type { get; set; }
    }
}
