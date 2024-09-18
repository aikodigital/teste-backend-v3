using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class PerformanceDTO
    {
        public int Id { get; set; }
        public int PlayId { get; set; }
        public PlayDTO? Play { get; set; }
        public int InvoiceId { get; set; }
        public InvoiceDTO? Invoice { get; set; }
        public int Audience { get; set; }

        public static implicit operator PerformanceDTO(PerformanceEntity entity)
        {
            var Dto = new PerformanceDTO
            {
                Id = entity.Id,
                PlayId = entity.PlayId,
                Audience = entity.Audience,
                Play = entity.Play == null ? null : new PlayDTO { Lines = entity.Play.Lines, Name = entity.Play.Name, Type = entity.Play.Type },
                InvoiceId = entity.InvoiceId,
                Invoice = entity.Invoice == null ? null : new InvoiceDTO { Id = entity.Invoice.Id, Customer = entity.Invoice.Customer },
            };

            return Dto;
        }
    }
}
