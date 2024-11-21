using Aplication.Services;
using ApprovalTests;
using ApprovalTests.Reporters;

namespace AplicationTest
{
    public class StatementServiceTests
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void ImpressaoTeste()
        {
            StatementService statementService = new();
            var resultado = statementService.Imprimir();
            Approvals.Verify(resultado);
        }
    }
}