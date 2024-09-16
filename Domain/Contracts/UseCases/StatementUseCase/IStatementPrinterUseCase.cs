using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;

namespace Domain.Contracts.UseCases.StatementUseCase
{
    public interface IStatementPrinterUseCase
    {
        string Print(Invoice invoice, Dictionary<string, Play> plays);
        string ConvertJsonToXml(string json);
        string ConvertJsonToTxt(string json);
    }
}
