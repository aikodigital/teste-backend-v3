using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Domain;

public class Invoice
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    private int _invoiceId;
    private string _customer;

    public Invoice() { }
    public Invoice(int invoiceId, string customer)
    {
        _invoiceId = invoiceId;
        _customer = customer;
    }

    public string Customer { get => _customer; set => _customer = value; }
    public int InvoiceId { get => _invoiceId; set => _invoiceId = value; }

    public override string ToString()
    {
        return $"{this._invoiceId} - {this._customer}";
    }
}
