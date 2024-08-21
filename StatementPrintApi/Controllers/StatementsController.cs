using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata;
using StatementPrintApi.Context;
using StatementPrintApi.Entities;
using Microsoft.EntityFrameworkCore;


namespace StatementPrintApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatementController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatements()
        {
            var statements = await _context.Statements.ToListAsync();
            return Ok(statements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatement(int id)
        {
            var statement = await _context.Statements.FindAsync(id);
            if (statement == null) return NotFound();
            return Ok(statement);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatement([FromBody] Statement statement)
        {
            _context.Statements.Add(statement);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStatement), new { id = statement.Id }, statement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatement(int id, [FromBody] Statement statement)
        {
            if (id != statement.Id) return BadRequest();

            _context.Entry(statement).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatement(int id)
        {
            var statement = await _context.Statements.FindAsync(id);
            if (statement == null) return NotFound();

            _context.Statements.Remove(statement);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}