using Xunit;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class TragedyChargeTests
    {
        [Fact]
        public void CalculateAudience_less30()
        {
            // performance com 20 espectadores em uma peça de tragédia com 2000 linhas
            var performance = new Performance("hamlet", 20);
            var play = new Play("Hamlet", 2000, "tragedy");
            var chargeStrategy = new TragedyCharge(); // Instância da classe correta

            // Executa o método para cálculo da cobrança
            var result = chargeStrategy.CalculateBilling(performance, play);

            // Verificação do valor retornado com base na lógica de cobrança
            Assert.Equal(20000, result); // Valor esperado: 2000 linhas * 10 = 20000
        }

        [Fact]
        public void CalculateAudienceGreater30_()
        {
            // performance com 40 espectadores em uma peça de tragédia com 2000 linhas
            var performance = new Performance("hamlet", 40);
            var play = new Play("Hamlet", 2000, "tragedy");
            var chargeStrategy = new TragedyCharge(); // Instância da classe correta

            // Executa o método para cálculo de cobrança
            var result = chargeStrategy.CalculateBilling(performance, play);

            // Verificação: O valor retornado deve ser igual a 30000
            Assert.Equal(30000, result); // Valor esperado: 2000 linhas * 10 + 1000 * (40 - 30) = 20000 + 10000 = 30000
        }
    }
}