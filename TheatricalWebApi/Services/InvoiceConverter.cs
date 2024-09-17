using System.Collections.Generic;
using System.Linq;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalWebApi.Factories;
using TheatricalWebApi.Models;

namespace TheatricalWebApi.Services;

public class InvoiceConverter
{
    public static Invoice ConvertToInvoice(InvoiceDTO invoiceDTO)
    {
        var performances = invoiceDTO.Performances
            .Select(p => new Performance(
                PlayFactory.CreatePlay(p.Play),
                p.Audience
            ))
            .ToList();

        return new Invoice(invoiceDTO.Customer, performances);
    }
}