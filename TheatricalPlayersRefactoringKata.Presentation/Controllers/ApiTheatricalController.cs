using Microsoft.AspNetCore.Mvc;

namespace TheatricalPlayersRefactoringKata.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiTheatricalController : ControllerBase
    {
        /// <summary>
        /// Retorna todas as performances teatrais.
        /// </summary>
        /// <returns>Lista de performances.</returns>
        [HttpGet]
        public IActionResult GetAllPerformances()
        {
            return Ok("Este método deve retornar uma lista de todas as performances.");
        }

        /// <summary>
        /// Retorna uma performance por ID.
        /// </summary>
        /// <param name="id">ID da performance.</param>
        /// <returns>Detalhes da performance.</returns>
        [HttpGet("{id}")]
        public IActionResult GetPerformanceById(int id)
        {
            return Ok($"Este método deve retornar os detalhes da performance com o ID {id}.");
        }

        /// <summary>
        /// Cria uma nova performance.
        /// </summary>
        /// <param name="model">Modelo da performance.</param>
        /// <returns>Confirmação da criação.</returns>
        [HttpPost]
        public IActionResult CreatePerformance([FromBody] ApiTheatrical model)
        {
            return Ok("Este método deve criar uma nova performance com base nos dados fornecidos.");
        }

        /// <summary>
        /// Atualiza completamente uma performance existente.
        /// </summary>
        /// <param name="id">ID da performance.</param>
        /// <param name="model">Modelo da performance atualizada.</param>
        /// <returns>Confirmação da atualização.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdatePerformance(int id, [FromBody] ApiTheatrical model)
        {
            return Ok($"Este método deve atualizar completamente a performance com o ID {id} com os dados fornecidos.");
        }

        /// <summary>
        /// Atualiza parcialmente uma performance existente.
        /// </summary>
        /// <param name="id">ID da performance.</param>
        /// <param name="patchModel">Modelo com as propriedades a serem atualizadas.</param>
        /// <returns>Confirmação da atualização parcial.</returns>
        [HttpPatch("{id}")]
        public IActionResult PatchPerformance(int id, [FromBody] ApiTheatrical patchModel)
        {
            return Ok($"Este método deve atualizar parcialmente a performance com o ID {id} com os dados fornecidos.");
        }

        /// <summary>
        /// Remove uma performance existente.
        /// </summary>
        /// <param name="id">ID da performance.</param>
        /// <returns>Confirmação da remoção.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePerformance(int id)
        {
            return Ok($"Este método deve excluir a performance com o ID {id}.");
        }
    }
}
