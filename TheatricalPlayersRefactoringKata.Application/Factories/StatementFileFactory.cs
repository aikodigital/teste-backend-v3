using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Files;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Factories
{
    public class StatementFileFactory
    {
        public static IStatementFile Create(string format)
        {
            return format.ToLower() switch
            {
                "xml" => new XmlFile(),
                _ => throw new ArgumentException($"Format '{format}' is not supported.")
            };
        }
    }
}
