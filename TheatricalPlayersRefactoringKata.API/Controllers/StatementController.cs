using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.OutputStrategies;
using TheatricalPlayersRefactoringKata.Factories;
using TheatricalPlayersRefactoringKata.Enums;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly IStatementOutputStrategy _outputStrategy;

        // Injeção de dependência da estratégia de formatação de output
        public StatementController(IStatementOutputStrategy outputStrategy)
        {
            _outputStrategy = outputStrategy;
        }

        /// <summary>
        /// Gera um "statement" (fatura detalhada) baseado em um Invoice e Plays
        /// </summary>
        /// <param name="invoice">Invoice com as performances</param>
        /// <param name="plays">Lista de peças relacionadas</param>
        /// <returns>Fatura formatada</returns>
        /// <response code="200">Statement gerado com sucesso</response>
        /// <response code="400">Dados inválidos fornecidos</response>
        [HttpPost]
        public ActionResult<string> GenerateStatement([FromBody] StatementRequest request)
        {
            if (request == null || request.Invoice == null || request.Plays == null || !request.Plays.Any())
            {
                return BadRequest("Dados inválidos fornecidos.");
            }

            try
            {
                // Inicializa o StatementPrinter com a estratégia de output injetada
                var statementPrinter = new StatementPrinter(_outputStrategy);

                // Gera o statement baseado no invoice e nas peças fornecidas
                var result = statementPrinter.Print(request.Invoice, request.Plays);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao gerar o statement: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Modelo para encapsular o Invoice e as Plays na requisição
    /// </summary>
    public class StatementRequest
    {
        public Invoice Invoice { get; set; }
        public Dictionary<string, Play> Plays { get; set; }
    }
}
