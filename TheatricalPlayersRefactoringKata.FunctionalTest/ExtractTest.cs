using ApprovalTests;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.API;
using TheatricalPlayersRefactoringKata.FunctionalTest.Config;
using Xunit;

namespace TheatricalPlayersRefactoringKata.FunctionalTest
{
    [Collection(nameof(IntegrationTestsAPIFixtureCollection))]
    public class ExtractTest
    {
        private readonly IntegrationTestsFixture<StartupTest> _testsFixture;

        public ExtractTest(IntegrationTestsFixture<StartupTest> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Theory]
        [InlineData(13)]
        public async Task TestXmlStatementExample(long invoiceId)
        {
            string value = await _testsFixture.GetExtractXML(invoiceId);
            Approvals.Verify(value);
        }
    }
}