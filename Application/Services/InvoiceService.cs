using Application.Commands.InvoiceCommands.CreateInvoiceCommands;
using Application.Commands.TheaterPlayCommands.CreateTheaterPlayCommands;
using Application.Services.Interfaces;
using Domain.DTOs;
using Shared.Handles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;

namespace Application.Services
{
    public class InvoiceService (ICommandHandler<CreateInvoiceCommand, InvoiceDTO> _CreateTheaterPlayCommandHandler) : IInvoiceService
    {
        public InvoiceDTO Create(InvoiceModelView invoce)
        {

            throw new NotImplementedException();
        }
    }
}
