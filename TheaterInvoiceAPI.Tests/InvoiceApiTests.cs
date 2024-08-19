using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace TheaterInvoiceAPI.Tests
{
    public class InvoiceApiTests : IClassFixture<WebApplicationFactory<TheaterInvoiceAPI.Program>>
    {
        private readonly HttpClient _client;

        public InvoiceApiTests(WebApplicationFactory<TheaterInvoiceAPI.Program> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task TestPrintInvoiceXmlFormat()
        {
            var request = new
            {
                customer = "John Doe",
                performances = new[]
                {
                    new { playId = "hamlet", audience = 55 }
                },
                plays = new
                {
                    hamlet = new { name = "Hamlet", type = "tragedy", lines = 10 }
                },
                format = "xml"
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Invoice/print", content);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("<customer>John Doe</customer>", responseBody);
            Assert.Contains("<performance play=\"Hamlet\"", responseBody);
        }


        [Fact]
        public async Task TestPrintInvoiceTextFormat()
        {
            var request = new
            {
                customer = "John Doe",
                performances = new[]
                {
                    new { playId = "hamlet", audience = 55 }
                },
                plays = new
                {
                    hamlet = new { name = "Hamlet", type = "tragedy", lines = 10 }
                },
                format = "text"
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Invoice/print", content);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("Statement for John Doe", responseBody);
            Assert.Contains("Hamlet: $350.00 (55 seats)", responseBody);
        }


        [Fact]
        public async Task TestPrintInvoiceInvalidFormat()
        {
            var request = new
            {
                customer = "John Doe",
                performances = new[]
                {
                    new { playId = "hamlet", audience = 55 }
                },
                plays = new
                {
                    hamlet = new { name = "Hamlet", type = "tragedy", lines = 10 }
                },
                format = "invalid_format"
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Invoice/print", content);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        public async Task TestPrintInvoiceDifferentData()
        {
            var request = new
            {
                customer = "Jane Doe",
                performances = new[]
                {
                    new { playId = "othello", audience = 40 },
                    new { playId = "hamlet", audience = 30 }
                },
                plays = new
                {
                    othello = new { name = "Othello", type = "tragedy", lines = 15 },
                    hamlet = new { name = "Hamlet", type = "tragedy", lines = 10 }
                },
                format = "xml"
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Invoice/print", content);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("<customer>Jane Doe</customer>", responseBody);
            Assert.Contains("<performance play=\"Othello\"", responseBody);
            Assert.Contains("<performance play=\"Hamlet\"", responseBody);
        }


        [Fact]
        public async Task TestPrintInvoicePerformance()
        {
            var request = new
            {
                customer = "John Doe",
                performances = new[]
                {
                    new { playId = "hamlet", audience = 55 },
                    new { playId = "othello", audience = 40 },
                    // Adicione mais itens para aumentar o volume de dados
                },
                plays = new
                {
                    hamlet = new { name = "Hamlet", type = "tragedy", lines = 10 },
                    othello = new { name = "Othello", type = "tragedy", lines = 15 },
                    // Adicione mais itens correspondentes
                },
                format = "xml"
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Invoice/print", content);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("<customer>John Doe</customer>", responseBody);
        }
    }
}
