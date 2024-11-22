using Microsoft.AspNetCore.Mvc;
using System;
using TheatricalPlayersRefactoringKata.Api.Model;
using TheatricalPlayersRefactoringKata.Application.Factories;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Api.Controllers
{
    /// <summary>
    /// Controller responsible for creating and managing statements, performances, and plays.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly IStatementService _statementService;
        private readonly IPerformanceService _performanceService;
        private readonly IPlayService _playService;

        public StatementController(IStatementService statementService, IPerformanceService performanceService, IPlayService playService)
        {
            _statementService = statementService;
            _performanceService = performanceService;
            _playService = playService;
        }

        /// <summary>
        /// Creates a new statement, calculating total costs and credits for each performance.
        /// </summary>
        /// <param name="statement">The statement model containing performances to be calculated.</param>
        /// <returns>A message indicating the success of the creation.</returns>
        /// <response code="200">Statement created successfully.</response>
        /// <response code="400">Invalid statement provided.</response>
        /// <response code="500">Internal server error if an unexpected error occurs.</response>
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateStatement([FromBody] StatementModel statement)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Invalid input.",
                        errors = ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage)
                    });
                }

                foreach (var performance in statement.Performances)
                {
                    var strategy = GenreStrategyFactory.Create(performance.Play.Type);

                    performance.Cost = strategy.CalculateCost(performance.Audience, performance.Play.Lines);
                    performance.Credits = strategy.CalculateCredits(performance.Audience);
                    performance.BasePrice = strategy.CalculateBasePrice(performance.Play.Lines);

                }

                statement.TotalCost = statement.Performances.Sum(performance => performance.Cost);
                statement.TotalCredits = statement.Performances.Sum(performance => performance.Credits);

                var Statement = StatementModel.MapTo(statement);

                var createdStatement = await _statementService.CreateStatementAsync(Statement);

                foreach (var performance in statement.Performances)
                {
                    performance.StatementId = createdStatement.Id;

                    performance.Play.PlayId = performance.PlayId;

                    var Performance = StatementModel.MapTo(performance);

                    var createdPerformance = await _performanceService.CreatePerformanceAsync(Performance);

                    var Play = StatementModel.MapTo(performance.Play);

                    var createdPlay = await _playService.CreatePlayAsync(Play);
                }

                return Ok("Statement has been successfully created.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }


    }
}
