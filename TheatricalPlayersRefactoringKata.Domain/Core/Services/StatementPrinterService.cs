using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IRepositories;
using TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IServices;
using TheatricalPlayersRefactoringKata.Domain.Utils;
namespace TheatricalPlayersRefactoringKata.Domain.Core.Services;

public class StatementPrinterService : IStatementPrinterService {

    private readonly IStatementPrinterRepository _repository;

    public StatementPrinterService(IStatementPrinterRepository Repository) {
        _repository = Repository;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays) {
        return _repository.Print(invoice, plays);
    }

}
