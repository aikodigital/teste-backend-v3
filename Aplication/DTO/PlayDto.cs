using CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTO
{
    public class PlayDto
    {
        public string Name { get; private set; }
        public int Lines { get; private set; }
        public PlayType Type { get; private set; }

        public PlayDto(string name, int lines, PlayType type)
        {
            Name = name;
            Lines = lines;
            Type = type;
        }
    }
}
