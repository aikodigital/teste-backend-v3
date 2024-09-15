using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayController : ControllerBase
    {
        private static List<Play> plays = new List<Play>();

        /// <summary>
        /// Retorna todas as peças.
        /// </summary>
        /// <returns>Lista de peças.</returns>
        /// <response code="200">Peças retornadas com sucesso</response>
        /// <response code="204">Nenhuma peça encontrada</response>
        [HttpGet]
        public ActionResult<IEnumerable<Play>> GetAllPlays()
        {
            if (!plays.Any())
            {
                return NoContent();
            }
            return Ok(plays);
        }

        /// <summary>
        /// Retorna uma peça específica com base no nome.
        /// </summary>
        /// <param name="name">Nome da peça</param>
        /// <returns>Peça correspondente</returns>
        /// <response code="200">Peça retornada com sucesso</response>
        /// <response code="404">Peça não encontrada</response>
        [HttpGet("{name}")]
        public ActionResult<Play> GetPlayByName(string name)
        {
            var play = plays.Find(p => p.Name == name);
            if (play == null)
            {
                return NotFound("Peça não encontrada.");
            }
            return Ok(play);
        }

        /// <summary>
        /// Cria uma nova peça.
        /// </summary>
        /// <param name="play">Objeto Play</param>
        /// <returns>Status da criação da peça</returns>
        /// <response code="201">Peça criada com sucesso</response>
        /// <response code="400">Dados inválidos fornecidos</response>
        /// <response code="500">Erro ao criar peça</response>
        [HttpPost]
        public ActionResult<Play> CreatePlay([FromBody] Play play)
        {
            if (play == null || string.IsNullOrWhiteSpace(play.Name) || play.Lines <= 0)
            {
                return BadRequest("Dados inválidos para a peça.");
            }

            try
            {
                plays.Add(play);
                return CreatedAtAction(nameof(GetPlayByName), new { name = play.Name }, play);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar peça: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza uma peça existente.
        /// </summary>
        /// <param name="name">Nome da peça a ser atualizada</param>
        /// <param name="updatedPlay">Dados atualizados da peça</param>
        /// <returns>Status da atualização da peça</returns>
        /// <response code="200">Peça atualizada com sucesso</response>
        /// <response code="404">Peça não encontrada</response>
        /// <response code="500">Erro ao atualizar peça</response>
        [HttpPut("{name}")]
        public ActionResult UpdatePlay(string name, [FromBody] Play updatedPlay)
        {
            var play = plays.Find(p => p.Name == name);
            if (play == null)
            {
                return NotFound("Peça não encontrada.");
            }

            play.Name = updatedPlay.Name;
            play.Lines = updatedPlay.Lines;
            play.Type = updatedPlay.Type;

            var updated = plays.Find(p => p.Name == updatedPlay.Name && p.Lines == updatedPlay.Lines && p.Type == updatedPlay.Type);
            if (updated == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar a peça.");
            }

            return Ok("Peça atualizada com sucesso.");
        }

        /// <summary>
        /// Remove uma peça existente.
        /// </summary>
        /// <param name="name">Nome da peça a ser removida</param>
        /// <returns>Status da remoção da peça</returns>
        /// <response code="200">Peça removida com sucesso</response>
        /// <response code="404">Peça não encontrada</response>
        /// <response code="500">Erro ao remover peça</response>
        [HttpDelete("{name}")]
        public ActionResult DeletePlay(string name)
        {
            var play = plays.Find(p => p.Name == name);
            if (play == null)
            {
                return NotFound("Peça não encontrada.");
            }

            plays.Remove(play);

            var deleted = plays.Find(p => p.Name == name);
            if (deleted != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao remover a peça.");
            }

            return Ok("Peça removida com sucesso.");
        }
    }
}