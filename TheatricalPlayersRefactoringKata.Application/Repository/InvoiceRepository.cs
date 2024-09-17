using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Play>> GetAllPlaysAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Play.ToListAsync();
            }
        }

        public Invoice GetInvoiceById(string invoiceId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Invoices.Where(x => x.InvoiceId.ToString().Equals(invoiceId)).FirstOrDefault();
            }
        }

        public async Task<Invoice> GetInvoiceByIdAsync(string invoiceId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Invoices.Where(x => x.InvoiceId.ToString().Equals(invoiceId)).FirstOrDefaultAsync();
            }
        }

        public List<InvoiceCalculeteSettings> GetInvoiceCalculeteSettings()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.InvoiceCalculeteSettings.ToList();
            }
        }

        public async Task<List<InvoiceCalculeteSettings>> GetInvoiceCalculeteSettingsAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.InvoiceCalculeteSettings.ToListAsync();
            }
        }

        public List<InvoiceCreditSettings> GetInvoiceCreditSettings()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            { 
                return context.InvoiceCreditSettings.ToList();
            }
        }

        public async Task<List<InvoiceCreditSettings>> GetInvoiceCreditSettingsAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.InvoiceCreditSettings.ToListAsync();
            }
        }

        public List<Performance> GetPerformancesByInvoiceId(string invoiceId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Performances.Where(x => x.InvoiceId.ToString().Equals(invoiceId)).ToList();
            }
        }

        public async Task<List<Performance>> GetPerformancesByInvoiceIdAsync(string invoiceId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Performances.Where(x => x.InvoiceId.ToString().Equals(invoiceId)).ToListAsync();
            }
        }
    }
}
