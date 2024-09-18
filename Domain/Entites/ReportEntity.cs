using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ReportEntity
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public InvoiceEntity? Invoice { get; set; }
        public int PlayId { get; set; }
        public PlayEntity? Play { get; set; }

        public int ReportCreditId { get; set; }
        public ReportCreditEntity? ReportCredit { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Seats { get; set; }
        public decimal Amount { get; set; }
        public static implicit operator ReportEntity(ReportDTO dto)
        {
            var entity = new ReportEntity
            {
                Id = dto.Id,
                Name = dto.Name,
                PlayId = dto.PlayId,
                Play = dto.Play == null ? null : new PlayEntity { Lines = dto.Play.Lines, Name = dto.Play.Name, Type = dto.Play.Type },
                InvoiceId = dto.InvoiceId,
                Invoice = dto.Invoice == null ? null : new InvoiceEntity { Customer = dto.Invoice.Customer },
                Seats = dto.Seats,
                Amount = dto.Amount,
                ReportCreditId = dto.ReportCreditId,      
            };

            return entity;
        }
    }
}
