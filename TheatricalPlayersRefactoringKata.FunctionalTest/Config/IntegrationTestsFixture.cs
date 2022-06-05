using System;
using System.Net.Http;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.API;
using Xunit;

namespace TheatricalPlayersRefactoringKata.FunctionalTest.Config
{
    [CollectionDefinition(nameof(IntegrationTestsAPIFixtureCollection))]
    public class IntegrationTestsAPIFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTest>> { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public HttpClient HttpClient;

        public IntegrationTestsFixture()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("https://localhost:44305/");
        }

        public async Task<string> GetExtractXML(long id)
        {
            var response = await this.HttpClient.GetAsync($"api/Invoice/extract/invoice-xml/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}