using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ReportCreditDTO
    {
        public int Id { get; set; }
        public decimal AmountTotal { get; set; }
        public int Credits
        {
            get; set;
        }
        public static implicit operator ReportCreditDTO(ReportCreditEntity entity)
        {
            var dto = new ReportCreditDTO
            {
                Id = entity.Id,
                AmountTotal = entity.AmountTotal,
                Credits = entity.Credits,
            };

            return dto;
        }

    }
}
