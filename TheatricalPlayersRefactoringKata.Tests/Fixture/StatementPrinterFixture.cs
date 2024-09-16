using Application.UseCases.StatementUseCase;
using Domain.Contracts.UseCases.StatementUseCase;

namespace TheatricalPlayersRefactoringKata.Tests.Fixture
{
    public class StatementPrinterFixture
    {
        public IStatementPrinterUseCase StatementPrinterUseCase { get; private set; }
        public IConvertUseCase ConvertUseCase { get; private set; }

        public StatementPrinterFixture()
        {
            StatementPrinterUseCase = new StatementPrinterUseCase();
            ConvertUseCase = new ConvertUseCase();
        }
    }
}
