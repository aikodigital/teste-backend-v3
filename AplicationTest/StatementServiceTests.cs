using Aplication.Services;

namespace AplicationTest
{
    public class StatementServiceTests
    {
        [Fact]
        public void Test1()
        {
            StatementService statementService = new StatementService();
            statementService.Print();
        }
    }
}