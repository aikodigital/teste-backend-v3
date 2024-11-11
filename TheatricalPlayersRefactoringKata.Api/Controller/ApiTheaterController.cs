using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Model;
using TheatricalPlayersRefactoringKata.Service;

namespace TheatricalPlayersRefactoringKata.Api.Controller;

[Route("Performance")]
[ApiController]
public class ApiTheaterController : ControllerBase
{
    private readonly PerformanceService _performanceService;

    public ApiTheaterController(PerformanceService performanceService)
    {
        _performanceService = performanceService;
    }

    /// <summary>
    /// Retorna a lista com todas as perfomances.
    /// </summary>
    /// <returns>Lista de performances.</returns>
    [HttpGet]
    [Route("ListAllPerformance")]
    public IActionResult GetAllPerformances()
    {
        var performances = _performanceService.GetAllPerformances();
        return Ok(performances);
    }

    /// <summary>
    /// Este método deve retornar os detalhes da performance com o ID {id}.
    /// </summary>
    /// <param name="id">ID da performance.</param>
    /// <returns>Detalhes da performance.</returns>
    [HttpGet]
    [Route("PerformanceById/{id}")]
    public IActionResult GetPerformanceById([FromRoute] int id)
    {
        var performance = _performanceService.GetPerformanceById(id);

        if (performance == null)
        {
            return BadRequest();
        }

        return Ok(performance);
    }

    /// <summary>
    /// Este método deve criar uma nova performance com base nos dados fornecidos.
    /// </summary>
    /// <param name="model">Modelo da performance.</param>
    /// <returns>Confirmação da criação.</returns>
    [HttpPost]
    [Route("CreatePerformance")]
    public IActionResult CreatePerformance([FromBody] Performance model)
    {
        var success = _performanceService.AddPerformance(model);

        if (success) return Created();
        
        return BadRequest();
    }

    /// <summary>
    /// Este método deve atualizar completamente a performance com o ID {id} com os dados fornecidos.
    /// </summary>
    /// <param name="id">ID da performance.</param>
    /// <param name="model">Modelo da performance atualizada.</param>
    /// <returns>Confirmação da atualização.</returns>
    [HttpPut]
    [Route("UpdatePerformance/{id}")]
    public IActionResult UpdatePerformance([FromRoute] int id, [FromBody] Performance model)
    {
        var success = _performanceService.UpdatePerformance(id, model);

        if (success) return Ok("Performance atualizado com sucesso.");
        
        return BadRequest();
    }

    /// <summary>
    /// Este método deve excluir a performance com o ID {id}.
    /// </summary>
    /// <param name="id">ID da performance.</param>
    /// <returns>Confirmação da remoção.</returns>
    [HttpDelete]
    [Route("DeletePerformace/{id}")]
    public IActionResult DeletePerformance([FromRoute] int id)
    {
        var result = _performanceService.DeletePerformance(id);

        if (result) return Ok($"Performance {id} excluída com sucesso.");

        return BadRequest();
    }
}
