using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public  class InvoiceEntity
    {
        public int Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public List<PerformanceEntity> Performances { get; set; }


        public static implicit operator InvoiceEntity(InvoiceDTO dto)
        {
            var Dto = new InvoiceEntity
            {
                Id = dto.Id,
                Customer = dto.Customer,
            };

            Dto.Performances = dto.Performances.Select(p => new PerformanceEntity
            {
                Id = p.Id,
                Audience = p.Audience,
                PlayId = p.PlayId,
                Play = p.Play == null ? null : new PlayEntity { Lines = p.Play.Lines, Name = p.Play.Name, Type = p.Play.Type, Id = p.PlayId }
            }).ToList();

            return Dto;
        }

    }
}
