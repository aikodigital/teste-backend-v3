using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;

namespace TheatricalPlayersRefactoringKata.Application.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public List<Play> GetAllPlays()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Play.ToList();
            }
        }

        public Invoice GetInvoiceById(string invoiceId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Invoices.Where(x => x.InvoiceId.ToString().Equals(invoiceId)).FirstOrDefault();
            }
        }

        public List<InvoiceCalculeteSettings> GetInvoiceCalculeteSettings()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.InvoiceCalculeteSettings.ToList();
            }
        }

        public List<InvoiceCreditSettings> GetInvoiceCreditSettings()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            { 
                return context.InvoiceCreditSettings.ToList();
            }
        }

        public List<Performance> GetPerformancesByInvoiceId(string invoiceId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Performances.Where(x => x.InvoiceId.ToString().Equals(invoiceId)).ToList();
            }
        }
    }
}
