using Application.Commands.Base;
using Application.Commands.TheaterPlayCommands.CreateTheaterPlayCommands;
using Domain.DTOs;
using Domain.Entites;
using Domain.Interfaces;
using Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TheaterPlayCommands.CreateInBatchTheaterPlayCommands
{
    public class CreateInBatchTheaterPlayCommandHandler(ITheaterPlayRepository _ITheaterPlayRepository) : CommandHandlerBase<CreateInBatchTheaterPlayCommand, TheaterPlayDTO>
    {
        public override async Task<ICommandResult<TheaterPlayDTO>> HandleAsync(CreateInBatchTheaterPlayCommand command)
        {
            var thatreplay = new List<TheaterPlayEntity>();
            foreach (var item in command.TheaterPlays)
            {
                thatreplay.Add(item.Dto());
            }
            var entity = _ITheaterPlayRepository.AddRange(thatreplay);
            
            var dtos = new List<TheaterPlayDTO>();
            foreach (var item in entity)
            {
                dtos.Add(item);
            }

            var result = new CommandResult<TheaterPlayDTO>(true, "TheaterPlayers create with sucess!");
            result.Dtos = dtos;
            return result;
        }
    }
}
