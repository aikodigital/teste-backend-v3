using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Common.Result;
using TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IRepositories;
using TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IServices;
using TheatricalPlayersRefactoringKata.Domain.Utils;
namespace TheatricalPlayersRefactoringKata.Domain.Core.Services;

public class StatementPrinterService : IStatementPrinterService {

    private readonly IStatementPrinterRepository _repository;

    public StatementPrinterService(IStatementPrinterRepository Repository) {
        _repository = Repository;
    }

    public Result<string> PrintText(Invoice invoice, Dictionary<string, Play> plays, Dictionary<Enum, IGenreStrategy> genres) {
        return _repository.PrintText(invoice, plays, genres);
    }

}
