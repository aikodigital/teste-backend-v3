using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ReportCreditEntity
    {
        public int Id { get; set; }
        public decimal AmountTotal { get; set; }
        public int Credits
        {
            get; set;
        }
        public static implicit operator ReportCreditEntity(ReportCreditDTO dto)
        {
            var entity = new ReportCreditEntity
            {
                Id = dto.Id,
                AmountTotal = dto.AmountTotal,
                Credits = dto.Credits
            };

            return entity;
        }

    }
}
