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
        public Type Type { get; private set; }

        public PlayDto(string name, int lines, Type type)
        {
            Name = name;
            Lines = lines;
            Type = type;
        }
    }
}
