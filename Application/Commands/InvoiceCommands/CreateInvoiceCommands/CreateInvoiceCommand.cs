using Domain.DTOs;
using Flunt.Notifications;
using Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Commands.InvoiceCommands.CreateInvoiceCommands
{
    public class CreateInvoiceCommand : Notifiable<Notification>, ICommand
    {
        
        public string Customer { get; set; } = string.Empty;
        public List<PerformanceDTO> Performances { get; set; }
        public void Validade()
        {
            throw new NotImplementedException();
        }

        public InvoiceDTO Dto()
        {
            var InvoiceDTO = new InvoiceDTO
            {
                Id = 0,
                Customer = Customer,
            };

            InvoiceDTO.Performances = Performances.Select(p => new PerformanceDTO
            {
                Id = p.Id,
                Audience = p.Audience,
                PlayId = p.PlayId,
                Play = p.Play
            }).ToList();


            return InvoiceDTO;

        }
    }
}
