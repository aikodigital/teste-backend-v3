using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Application.Exceptions
{
    public class InvalidPrintTypeException : Exception
    {
        public InvalidPrintTypeException(string printType) : base($"Unknown print type: {printType}")
        {
        }
    }
}
