using Aplication.Services;

namespace AplicationTest
{
    public class StatementServiceTests
    {
        [Fact]
        public void ImpressaoTeste()
        {
            StatementService statementService = new();
            statementService.Imprimir();
        }
    }
}