using MediatR;
using Microsoft.AspNetCore.Mvc;
using TS.Application.Plays.Commands.AddPlays.Request;
using TS.Application.Plays.Commands.DeletePlays.Request;
using TS.Application.Plays.Commands.UpdatePlays.Request;
using TS.Application.Plays.Queries.GetAllPlays.Request;
using TS.Application.Plays.Queries.GetAllPlays.Response;
using TS.Application.Plays.Queries.GetPlays.Request;
using TS.Presentation.ViewModels;

namespace TS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayController : BaseController
    {
        /// <summary>
        /// Get all Plays
        /// </summary>
        /// <param name="term">Enter registration number</param>
        /// <returns>If there is a Play, return the Play, if not, return empty list</returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllPlaysResponse>))]
        public async Task<ActionResult> GetAll(string? term)
        {
            try
            {
                var request = new GetAllPlaysRequest { Term = term };
                var result = await Mediator!.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieve Plays by id
        /// </summary>
        /// <param name="id">Play identifier</param> 
        /// <returns>If there is a Play, return the Play, if not, return null</returns>
        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllPlaysResponse>))]
        public async Task<ActionResult> Get(long id)
        {
            try
            {
                var request = new GetPlaysRequest { Id = id };
                var result = await Mediator!.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Registration creation
        /// </summary>
        /// <param name="viewModel">Object representing an Play</param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        public async Task<ActionResult> Create([FromBody] AddPlaysViewModel viewModel)
        {
            try
            {
                var request = new AddPlaysRequest
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Type = viewModel.Type,
                    Lines = viewModel.Lines
                };

                await Mediator!.Send(request);

                return Ok("Peça de teatro criada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Registration update
        /// </summary>
        /// <param name="viewModel">Object representing an Play</param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        public async Task<ActionResult> Update([FromForm] UpdatePlaysViewModel viewModel)
        {
            try
            {
                var request = new UpdatePlaysRequest
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Type = viewModel.Type,
                    Lines = viewModel.Lines
                };

                await Mediator!.Send(request);

                return Ok("Peça de teatro atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete Play
        /// </summary>
        /// <param name="id">Play identifier</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var request = new DeletePlaysRequest
                {
                    Id = id
                };

                await Mediator!.Send(request);

                return Ok("Peça de teatro excluída com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}