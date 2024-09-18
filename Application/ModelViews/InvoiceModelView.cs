using Domain.DTOs;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata;

public class InvoiceModelView
{


    public string Customer { get ; set ; }
    public List<PerformanceModelView> Performances { get ; set ; }

    public InvoiceModelView()
    {

    }

    public InvoiceDTO DTO()
    {
        var dto = new InvoiceDTO
        {
            Customer = Customer,
            Performances = Performances.Select(p => p.DTO()).ToList()
        };
        return dto;
    }

}
