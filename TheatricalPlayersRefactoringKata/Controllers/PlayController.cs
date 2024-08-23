using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Repository.Plays;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [ApiController]
    [Route("plays")]
    public class PlayController : ControllerBase
    {
        public readonly IPlayRepository _playRepositorio;
        public PlayController(IPlayRepository playRepositorio)
        {
            _playRepositorio = playRepositorio;
        }

        [HttpGet("{playName}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByName(string playName)
        {
            try
            {
                return Ok(await _playRepositorio.GetByName(playName));
            }
            catch (ArgumentOutOfRangeException ex) { return NotFound(ex.Message); }
            catch (DbUpdateException ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var types = await _playRepositorio.GetAll();
                return types.Any() ? Ok(types) : NoContent();
            }
            catch (DbUpdateException ex) { return StatusCode(500, ex.Message); }
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] Play play)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("model is not valid");

                var type = await _playRepositorio.Create(play);
                return Created("", type);
            }
            catch (ArgumentOutOfRangeException ex) { return NotFound(ex.Message); }
            catch (DbUpdateException ex) { return StatusCode(500, ex.Message); }
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] Play playEdited)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("model is not valid");

                var type = await _playRepositorio.Update(playEdited);
                return Accepted(type);
            }
            catch (ArgumentOutOfRangeException ex) { return NotFound(ex.Message); }
            catch (DbUpdateException ex) { return StatusCode(500, ex.Message); }
        }

        [HttpDelete("{playName}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(string playName)
        {
            try
            {
                _playRepositorio.DeleteByName(playName);
                return Accepted("play deleted");
            }
            catch (ArgumentOutOfRangeException ex) { return NotFound(ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

    }
}
