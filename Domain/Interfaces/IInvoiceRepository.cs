using Domain.Entites;
using Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IInvoiceRepository : IRepository<InvoiceEntity>
    {
    }
}
