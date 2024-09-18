using Application.Commands.Base;
using Domain.DTOs;
using Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TheaterPlayCommands.CreateTheaterPlayCommands
{
    internal class CreateTheaterPlayCommandHandler : CommandHandlerBase<CreateTheaterPlayCommand, TheaterPlayDTO>
    {
        public override Task<ICommandResult<TheaterPlayDTO>> HandleAsync(CreateTheaterPlayCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
