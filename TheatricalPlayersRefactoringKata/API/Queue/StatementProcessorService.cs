using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.API.DTOs;
using TheatricalPlayersRefactoringKata.API.Data;
using TheatricalPlayersRefactoringKata.Models;

using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.API.Queue
{
    public class StatementProcessorService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public StatementProcessorService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope()) 
                {
                    var queue = scope.ServiceProvider.GetRequiredService<ITheaterStatementProcessingQueue>(); 

                    var invoiceJson = await queue.DequeueAsync();

                    var invoiceRequest = JsonSerializer.Deserialize<TheaterInvoiceRequestDTO>(invoiceJson);

                    if (invoiceRequest != null)
                    {
                        var invoice = new InvoiceRecordEntity
                        {
                            Customer = invoiceRequest.Customer,
                            Performances = invoiceRequest.Performances, 
                            StatementXml = "<xml>Generated XML</xml>",
                            ProcessedAt = DateTime.UtcNow
                        };

                        var dbContext = scope.ServiceProvider.GetRequiredService<TheaterAppDbContext>();
                        try
                        {
                            dbContext.Invoices.Add(invoice);
                            await dbContext.SaveChangesAsync();
                            Console.WriteLine("Invoice salva no banco.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao salvar invoice: {ex.Message}");
                        }

                        Console.WriteLine($"Invoice processed and saved: {invoice.Customer}");
                    }
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
