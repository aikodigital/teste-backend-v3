using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Data;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Repository;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [ApiController]
    [Route("playtypes")]
    public class PlayTypeController : ControllerBase
    {
        public readonly IPlayTypeRepository _playTypeRepositorio;
        public PlayTypeController(IPlayTypeRepository playTypeRepositorio)
        {
            _playTypeRepositorio = playTypeRepositorio;
        }

        [HttpGet("{typeName}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByName(string typeName)
        {
            try
            {
                PlayTypes type = await _playTypeRepositorio.GetByName(typeName);
                return Ok(type);
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
                var types = await _playTypeRepositorio.GetAll();
                return types.Any() ? Ok(types) : NoContent();
            }
            catch (DbUpdateException ex) { return StatusCode(500, ex.Message); }
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] PlayTypes playTypeEdited)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("model is not valid");

                var type = await _playTypeRepositorio.Create(playTypeEdited);
                return Created($"getByUser/{type.Name}", type);
            }
            catch (ArgumentOutOfRangeException ex) { return NotFound(ex.Message); }
            catch (DbUpdateException ex) { return StatusCode(500, ex.Message); }
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] PlayTypes playTypeEdited)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("model is not valid");

                var type = await _playTypeRepositorio.Update(playTypeEdited);
                return Accepted(type);
            }
            catch (ArgumentOutOfRangeException ex) { return NotFound(ex.Message); }
            catch (DbUpdateException ex) { return StatusCode(500, ex.Message); }
        }

        [HttpDelete("{typeName}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(string typeName)
        {
            try
            {
                _playTypeRepositorio.DeleteByName(typeName);
                return Accepted("play type deleted");
            }
            catch (ArgumentOutOfRangeException ex) { return NotFound(ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}
