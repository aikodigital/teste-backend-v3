using Application.Commands.TheaterPlayCommands.CreateInBatchTheaterPlayCommands;
using Application.Commands.TheaterPlayCommands.CreateTheaterPlayCommands;
using Application.ModelViews;
using Application.Services.Interfaces;
using Domain.DTOs;
using Shared.Handles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TheaterPlayService (ICommandHandler<CreateTheaterPlayCommand, TheaterPlayDTO> _CreateTheaterPlayCommandHandler,
        ICommandHandler<CreateInBatchTheaterPlayCommand, TheaterPlayDTO> _CreateTheaterBatchPlayCommandHandler) : ITheaterPlayService
    {
        public async Task<TheaterPlayDTO> create(TheaterPlayModelView theaterPlay)
        {
            var commad = new CreateTheaterPlayCommand
            {
                Name = theaterPlay.Name,
                Play = theaterPlay.Play,
            };

            var result = await _CreateTheaterPlayCommandHandler.HandleAsync(commad);

            return result.Dto;
        }

        public async Task<IEnumerable<TheaterPlayDTO>> createInBatch(IEnumerable<TheaterPlayModelView> theaterPlay)
        {

            var CreatComand = theaterPlay.Select(p => new CreateTheaterPlayCommand
            {
                Name = p.Name,
                Play = p.Play,
            });

            var commadBatch = new CreateInBatchTheaterPlayCommand
            {
                TheaterPlays = CreatComand.ToList(),
            };

            var result = await _CreateTheaterBatchPlayCommandHandler.HandleAsync(commadBatch);

            return result.Dtos;
        }
    }
}
