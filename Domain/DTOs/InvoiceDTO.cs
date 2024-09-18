using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public  class InvoiceDTO
    {
        public int Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public List<PerformanceDTO> Performances { get; set; }

        public static implicit operator InvoiceDTO(InvoiceEntity entity)
        {
            var Dto = new InvoiceDTO
            {
                Id = entity.Id,
                Customer = entity.Customer,
            };

            Dto.Performances = entity.Performances.Select(p => new PerformanceDTO
            {
                Id = p.Id,
                Audience = p.Audience,
                Play = p.Play == null ? null : new PlayDTO { Lines = p.Play.Lines, Name = p.Play.Name, Type = p.Play.Type } 
            }).ToList();

            return Dto;
        }
    }
}
