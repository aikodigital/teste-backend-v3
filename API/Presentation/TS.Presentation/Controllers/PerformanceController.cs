using Microsoft.AspNetCore.Mvc;
using TS.Application.Performances.Commands.AddPerformances.Request;
using TS.Application.Performances.Commands.DeletePerformances.Request;
using TS.Application.Performances.Commands.UpdatePerformances.Request;
using TS.Application.Performances.Queries.GetAllPerformances.Request;
using TS.Application.Performances.Queries.GetAllPerformances.Response;
using TS.Application.Performances.Queries.GetPerformances.Request;
using TS.Presentation.ViewModels;

namespace TS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : BaseController
    {
        /// <summary>
        /// Get all Performances
        /// </summary>
        /// <param name="term">Enter registration number</param>
        /// <returns>If there is a Performance, return the Performance, if not, return empty list</returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllPerformancesResponse>))]
        public async Task<ActionResult> GetAll(string? term)
        {
            try
            {
                var request = new GetAllPerformancesRequest { Term = term };
                var result = await Mediator!.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieve Performances by id
        /// </summary>
        /// <param name="id">Performance identifier</param> 
        /// <returns>If there is a Performance, return the Performance, if not, return null</returns>
        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllPerformancesResponse>))]
        public async Task<ActionResult> Get(long id)
        {
            try
            {
                var request = new GetPerformancesRequest { Id = id };
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
        /// <param name="viewModel">Object representing an Performance</param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        public async Task<ActionResult> Create([FromBody] AddPerformancesViewModel viewModel)
        {
            try
            {
                var request = new AddPerformancesRequest
                {
                    Id = viewModel.Id,
                    PlayId = viewModel.PlayId,
                    Audience = viewModel.Audience
                };

                await Mediator!.Send(request);

                return Ok("Apresentação criada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Registration update
        /// </summary>
        /// <param name="viewModel">Object representing an Performance</param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        public async Task<ActionResult> Update([FromForm] UpdatePerformancesViewModel viewModel)
        {
            try
            {
                var request = new UpdatePerformancesRequest
                {
                    Id = viewModel.Id,
                    PlayId = viewModel.PlayId,
                    Audience = viewModel.Audience
                };

                await Mediator!.Send(request);

                return Ok("Apresentação atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete Performance
        /// </summary>
        /// <param name="id">Performance identifier</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var request = new DeletePerformancesRequest
                {
                    Id = id
                };

                await Mediator!.Send(request);

                return Ok("Apresentação excluída com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}