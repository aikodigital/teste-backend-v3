using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

using TheatricalPlayersRefactoringKata.API.Data;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    [SwaggerTag("Endpoints relacionados à geração de extratos das Peças Teatrais.")]
    public class ExtratoController : ControllerBase
    {
        private readonly Context _context;
        private IConfiguration _config;

        public ExtratoController(Context context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos extratos", Description = "Retorna a lista de todos extratos gerados.")]
        public async Task<IActionResult> GetExtratos()
        {
            var extratos = await _context.Extrato.ToListAsync();
            if (extratos.IsNullOrEmpty())
            {
                return NotFound();
            }
            return new ObjectResult(extratos);
        }

        [HttpGet, Authorize, Route("{id}")]
        [SwaggerOperation(Summary = "Lista extrato por id", Description = "Retorna extrato por id consultado.")]
        public async Task<IActionResult> GetExtrato(int? id)
        {
            var extrato = await _context.Extrato.FirstOrDefaultAsync(u => u.Id == id);
            if (extrato == null)
            {
                return NotFound();
            }
            return new ObjectResult(extrato);
        }

        [HttpGet, Authorize, Route("nome/{nome}")]
        [SwaggerOperation(Summary = "Lista extratos por nome", Description = "Retorna lista de extratos que contenham parte do nome buscado.")]
        public async Task<IActionResult> GetExtratoPorNome(string? nome)
        {
            var extratos = await _context.Extrato.Where(u => u.PlayName.Contains(nome)).ToListAsync();
            if (extratos.IsNullOrEmpty())
            {
                return NotFound();
            }
            return new ObjectResult(extratos);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra extrato", Description = "Realiza cadastro de um novo extrato.")]
        public async Task<IActionResult> PostExtrato(Extrato extrato)
        {
            _context.Extrato.Add(extrato);
            await _context.SaveChangesAsync();
            return Ok("Extrato cadastrado com sucesso!");
        }

        [HttpPut, Authorize]
        [SwaggerOperation(Summary = "Altera extrato", Description = "Altera cadastro de um extrato.")]
        public async Task<IActionResult> PutExtrato(Extrato extrato)
        {
            _context.Extrato.Update(extrato);
            await _context.SaveChangesAsync();
            return Ok("Extrato alterado com sucesso!");
        }

        [HttpDelete, Authorize, Route("{id}")]
        [SwaggerOperation(Summary = "Deleta extrato", Description = "Deleta cadastro de um extrato.")]
        public async Task<IActionResult> DeleteExtrato(int? id)
        {
            var extrato = await _context.Extrato.FirstOrDefaultAsync(u => u.Id == id);
            if (extrato == null)
                return NotFound();
            _context.Extrato.Remove(extrato);
            await _context.SaveChangesAsync();
            return Ok("Extrato deletado com sucesso");
        }
    }
}
