using Aplication.DTO;
using Aplication.Interfaces;
using Aplication.Services.Calculators;
using Aplication.Services.Formatters;
using Aplication.Services.Interfaces;
using AutoMapper;
using CrossCutting;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entity;

namespace Aplication.Services
{
    public class StatementService : IStatementService
    {
        PlayService playService = new PlayService();

        private readonly TesteBackendV3DbContext _context;
        private readonly IMapper _mapper;

        public StatementService() { }

        public StatementService(TesteBackendV3DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task InsertInvoice(InvoiceDto dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        private List<PerformanceDto> ListAllPerformances()
        {
            List<PerformanceDto> performances = new()
            {
                new(GetPlayByName("Hamlet"), 55),
                new(GetPlayByName("You Like"), 35),
                new(GetPlayByName("Othello"), 40),
                new(GetPlayByName("Henry"), 20),
                new(GetPlayByName("John"), 39)
                };
            return performances;
        }

        private List<PerformanceDto> GetPerformancesByName(params string[] names)
        {
            var AllPerformances = ListAllPerformances();
            var perfs = names.Select(name => AllPerformances.FirstOrDefault(perf => perf.Play.Name.Contains(name)))
                .ToList();
            return perfs!;
        }

        private PlayDto GetPlayByName(string name)
        => playService.GetPlays().FirstOrDefault(x => x.Name.Contains(name))!;

        public InvoiceDto ObterInvoiceBigCo()
        => new()
        {
            Customer = "BigCo",
            Performances = GetPerformancesByName("Hamlet", "As You Like",
            "Othello")
        };

        public InvoiceDto ObterInvoiceBigCo2()
        => new()
        {
            Customer = "BigCo",
            Performances = GetPerformancesByName("Hamlet", "As You Like", "Othello", "Henry", "John", "Henry")
        };

        public string Print(InvoiceDto invoice, IInvoiceFormatter formatter)
        {
            var (performances, valorTotal, valorCreditos) = InvoiceProcessor.Processar(invoice);
            return formatter.Format(invoice, valorTotal, valorCreditos, performances);
        }

        public async Task<List<InvoiceDto>> GetInvoices()
        {
            var invoices = await _context!.Invoices!.Include(i=> i.Performances)
                .ThenInclude(p=> p.Play)
                .ToListAsync();
            var invoicesDto = _mapper.Map<List<InvoiceDto>>(invoices!);
            return invoicesDto;
        }
    }
}
