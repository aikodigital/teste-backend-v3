using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.OutputStrategies;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceController : ControllerBase
    {
        private static List<Performance> performances = new List<Performance>();

        /// <summary>
        /// Recupera todas as Performances
        /// </summary>
        /// <returns>Mensagem de erro ou sucesso</returns>
        /// <response code="200">Retorno de todas as performances</response>
        /// <response code="204">Nenhuma performance encontrada</response>
        [HttpGet]
        public ActionResult<IEnumerable<Performance>> GetAllPerformances()
        {
            if (!performances.Any())
            {
                return NoContent();
            }

            return Ok(performances);
        }

        /// <summary>
        /// Recupera uma Performance especifica pelo ID informado
        /// </summary>
        /// <returns>Mensagem de erro ou sucesso</returns>
        /// <response code="200">Retorno da performance especificada</response>
        /// <response code="404">Performance não encontrada</response>
        [HttpGet("{id}")]
        public ActionResult<Performance> GetPerformanceById(string id)
        {
            var performance = performances.Find(p => p.PlayId == id);
            if (performance == null)
            {
                return NotFound();
            }
            return Ok(performance);
        }

        /// <summary>
        /// Cria uma Performance com base nos parametros fornecidos
        /// </summary>
        /// <returns>Mensagem de erro ou sucesso</returns>
        /// <response code="201">Performance criada com sucesso!</response>
        /// <response code="400">Dados da performance são inválidos</response>
        /// <response code="500">Erro na criação da performance</response>
        [HttpPost]
        public ActionResult<Performance> CreatePerformance([FromBody] Performance performance)
        {
            if (performance == null || string.IsNullOrWhiteSpace(performance.PlayId) || performance.Audience <= 0)
            {
                return BadRequest("Dados da performance são inválidos.");  // Retorna 400 Bad Request
            }

            try
            {
                performances.Add(performance);

                if (performances.Any(p => p.PlayId == performance.PlayId))
                {
                    return CreatedAtAction(nameof(GetPerformanceById), new { id = performance.PlayId }, performance);  // Retorna 201 Created
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar a performance.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro no servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza uma Performance com base nos parametros fornecidos
        /// </summary>
        /// <returns>Mensagem de erro ou sucesso</returns>
        /// <response code="200">Performance atualizada com sucesso!</response>
        /// <response code="404">Performance não encontrada!</response>
        /// <response code="500">Erro na atualização da performance da performance!</response>
        [HttpPut("{id}")]
        public ActionResult UpdatePerformance(string id, [FromBody] Performance updatedPerformance)
        {
            var performance = performances.Find(p => p.PlayId == id);

            if (performance == null)
            {
                return NotFound("Performance não encontrada.");
            }
            performance.PlayId = updatedPerformance.PlayId;
            performance.Audience = updatedPerformance.Audience;

            var updated = performances.Find(p => p.PlayId == updatedPerformance.PlayId && p.Audience == updatedPerformance.Audience);
            if (updated == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar a performance.");
            }

            return Ok("Performance atualizada com sucesso.");
        }

        /// <summary>
        /// Remove uma Performance com base nos parametros fornecidos
        /// </summary>
        /// <returns>Mensagem de erro ou sucesso</returns>
        /// <response code="200">Performance removida com sucesso!</response>
        /// <response code="404">Performance não encontrada!</response>
        /// <response code="500">Erro ao remover a performance!</response>
        [HttpDelete("{id}")]
        public ActionResult DeletePerformance(string id)
        {
            var performance = performances.Find(p => p.PlayId == id);

            if (performance == null)
            {
                return NotFound("Performance não encontrada.");
            }

            performances.Remove(performance);

            var deleted = performances.Find(p => p.PlayId == id);
            if (deleted != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao remover a performance.");
            }

            return Ok("Performance removida com sucesso.");
        }
    }
}
