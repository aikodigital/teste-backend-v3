using System.Xml;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.API.ProcessBackground;

public class ProcessInvoiceQueue : BackgroundService, IHostedService
{
    private readonly TimeSpan _period = TimeSpan.FromSeconds(15);
    private readonly IServiceProvider _serviceProvider;

    public ProcessInvoiceQueue(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(_period);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            using var scope = _serviceProvider.CreateScope();

            ICustomerStatementRepository _iCustomerStatementRepository = scope.ServiceProvider.GetRequiredService<ICustomerStatementRepository>();
            ICustomerStatementProcessRepository _iCustomerStatementProcessRepository = scope.ServiceProvider.GetRequiredService<ICustomerStatementProcessRepository>();

            await ProcessAndCreateFile(_iCustomerStatementRepository, _iCustomerStatementProcessRepository);
        }
    }

    protected async Task ProcessAndCreateFile(ICustomerStatementRepository _iCustomerStatementRepository, ICustomerStatementProcessRepository _iCustomerStatementProcessRepository)
    {
        string pathApplication = AppDomain.CurrentDomain.BaseDirectory;

        var statementNotProcess = (await _iCustomerStatementRepository.GetByFilter(x => !x.CustomerStatementProcess.Any()));
        foreach (var process in statementNotProcess)
        {
            string filePath = Path.Combine(pathApplication, "XML");

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath = Path.Combine(filePath, $"{Guid.NewGuid()}.xml");

            XmlWriterSettings settings = new()
            {
                Indent = true
            };

            XDocument doc = new XDocument(new XElement("Statement",
                    new XElement("Customer", $"Statement for {process.Customer}")
                )
            );

            XElement root = doc.Element("Statement");

            foreach (var play in process.CustomerPlaysStatement)
            {
                XElement playElement = new XElement("Play",
                    new XElement("Title", $"{play.Play.Name}: "),
                    new XElement("Amount", $"${Convert.ToDecimal(play.Amount)/100}"),
                    new XElement("Seats", $"({play.TotalSeats}) seats")
                );

                root.Add(playElement);
            }

            root.Add(
                new XElement("TotalAmount", $"Amount owed is ${Convert.ToDecimal(process.TotalAmount) / 100}"),
                new XElement("Credits", $"You earned {process.VolumeCredits} credits")
            );

            doc.Save(filePath);

            await _iCustomerStatementProcessRepository.AddAsync(new CustomerStatementProcess()
            {
                CustomerStatementId = process.Id,
                Process = true
            });
        }
    }
}