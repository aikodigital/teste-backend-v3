using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class PerformanceEntity
    {
        public int Id { get; set; }
        public int PlayId { get; set; }
        public PlayEntity? Play { get; set; }
        public int InvoiceId { get; set; }
        public InvoiceEntity? Invoice { get; set; }

        public int Audience { get; set; }


        public static implicit operator PerformanceEntity(PerformanceDTO dto)
        {
            var entity = new PerformanceEntity
            {
                Id = dto.Id,
                PlayId = dto.PlayId,
                Audience = dto.Audience,
                Play = dto.Play == null ? null : new PlayEntity { Lines = dto.Play.Lines, Name = dto.Play.Name, Type = dto.Play.Type },
                InvoiceId = dto.InvoiceId,
                Invoice = dto.Invoice == null ? null : new InvoiceEntity { Id = dto.Invoice.Id, Customer = dto.Invoice.Customer },
            };

            return entity;
        }
    }
}
