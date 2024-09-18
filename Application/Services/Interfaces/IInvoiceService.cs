using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;

namespace Application.Services.Interfaces
{
    public interface IInvoiceService
    {
        public InvoiceDTO Create(InvoiceModelView invoce);
    }
}
