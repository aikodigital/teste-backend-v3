using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure;

namespace TheatricalPlayersRefactoringKata.Application
{
    public class StatementFileService
    {
        public async Task GenerateStatementFile(string content, string directoryPath, string fileName, IStatementFile file)
        {
            await file.SaveFileAsync(directoryPath, fileName, content);
        }
    }
}
