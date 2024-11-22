using Aplication.DTO;
using Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Interfaces
{
    public  interface IStatementService
    {
        InvoiceDto ObterInvoiceBigCo2();
        string Print(InvoiceDto invoice, IInvoiceFormatter formatter);
    }
}
