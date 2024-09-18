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
    public class TheaterPlayService (ICommandHandler<CreateTheaterPlayCommand, TheaterPlayDTO> _CreateTheaterPlayCommandHandler) : ITheaterPlayService
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
    }
}
