using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Models;

public class InvoiceModel
{
    private string _id;
    private string _customer;
    private List<PerformanceModel> _performances;

    public string Id { get => _id; set => _id = value; }
    public string Customer { get => _customer; set => _customer = value; }
    public List<PerformanceModel> Performances { get => _performances; set => _performances = value; }

    public InvoiceModel(string customer, List<PerformanceModel> performances)
    {
        _customer = customer;
        _performances = performances;
    }

    public static InvoiceModel ConvertToModel(Invoice invoice)
    {
        return new InvoiceModel(invoice.Customer, PerformanceModel.ConvertToModel(invoice.Performances))
        {
            Id = invoice.Id
        };
    }

    public static List<InvoiceModel> ConvertToModels(IEnumerable<Invoice> invoice)
    {
        return invoice.ToList().ConvertAll(invoiceModel =>
            new InvoiceModel(invoiceModel.Customer, PerformanceModel.ConvertToModel(invoiceModel.Performances))
            {
                Id = invoiceModel.Id
            });
    }

    public static Invoice ConvertToEntity(InvoiceModel invoiceModel)
    {
        return new Invoice
        {
            Id = invoiceModel.Id,
            Customer = invoiceModel.Customer,
            Performances = PerformanceModel.ConvertToEntity(invoiceModel.Performances)
        };
    }
}
