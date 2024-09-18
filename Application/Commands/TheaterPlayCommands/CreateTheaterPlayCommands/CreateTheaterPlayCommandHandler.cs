using Application.Commands.Base;
using Domain.DTOs;
using Domain.Interfaces;
using Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TheaterPlayCommands.CreateTheaterPlayCommands
{
    public class CreateTheaterPlayCommandHandler (ITheaterPlayRepository _ITheaterPlayRepository): CommandHandlerBase<CreateTheaterPlayCommand, TheaterPlayDTO>
    {
        public override async Task<ICommandResult<TheaterPlayDTO>> HandleAsync(CreateTheaterPlayCommand command)
        {
            
           var entity =await _ITheaterPlayRepository.AddAsync(command.Dto());

           var result = new  CommandResult<TheaterPlayDTO>(true, "TheaterPlayers create with sucess!");
            result.Dto = entity;
           return result;
        }
    }
}
