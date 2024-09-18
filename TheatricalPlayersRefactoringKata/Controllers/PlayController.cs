﻿using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayController : ControllerBase
    {
        private readonly IPlayService _playsService;

        public PlayController(IPlayService playsService)
        {
            _playsService = playsService;
        }

        [HttpPost("Create")]
        public Task<ActionResult> Create(string name, int lines, int type)
        {
            try
            {
                return _playsService.Create(new PlayModel(name, lines, (TypePlay)type));
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("GetByName")]
        public Task<ActionResult> GetByName(string name)
        {
            try
            {
                return _playsService.GetByName(name);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("GetAll")]
        public Task<ActionResult> GetAll()
        {
            try
            {
                return _playsService.GetAll();
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("Update")]
        public Task<ActionResult> Update(PlayModel play)
        {
            try
            {
                return _playsService.Update(play);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("Delete")]
        public Task<ActionResult> Delete(string id)
        {
            try
            {
                return _playsService.Delete(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
