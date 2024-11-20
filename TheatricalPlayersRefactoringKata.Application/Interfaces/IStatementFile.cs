using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IStatementFile
    {
        Task SaveFileAsync(string directoryPath, string fileName, string content);
    }
}
