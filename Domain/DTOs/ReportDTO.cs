using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public InvoiceDTO? Invoice { get; set; }
        public int PlayId { get; set; }
        public PlayDTO? Play { get; set; }
        public int ReportCreditId { get; set; }
        public ReportCreditDTO? ReportCredit { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Seats { get; set; }
        public decimal Amount { get; set; }
        public static implicit operator ReportDTO(ReportEntity entity)
        {
            var dto = new ReportDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                PlayId = entity.PlayId,
                Play = entity.Play == null ? null : new PlayDTO { Lines = entity.Play.Lines, Name = entity.Play.Name, Type = entity.Play.Type },
                InvoiceId = entity.InvoiceId,
                Invoice = entity.Invoice == null ? null : new InvoiceDTO { Customer = entity.Invoice.Customer },
                Seats = entity.Seats,
                Amount = entity.Amount,
                ReportCreditId = entity.ReportCreditId,
                ReportCredit = entity.ReportCredit == null ? null : new ReportCreditDTO { Id = entity.ReportCredit.Id, AmountTotal = entity.ReportCredit.AmountTotal, Credits = entity.ReportCredit.Credits },
            };

            return dto;
        }
    }
}
