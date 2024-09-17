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
    [SwaggerTag("Endpoints relacionados à geração das exibições de Peças Teatrais.")]
    public class PlayController : ControllerBase
    {
        private readonly Context _context;
        private IConfiguration _config;

        public PlayController(Context context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas peças", Description = "Retorna a lista de todas peças geradas.")]
        public async Task<IActionResult> GetPlays()
        {
            var pecas = await _context.Play.ToListAsync();
            if (pecas.IsNullOrEmpty())
            {
                return NotFound();
            }
            return new ObjectResult(pecas);
        }

        [HttpGet, Authorize, Route("nome/{nome}")]
        [SwaggerOperation(Summary = "Lista peças por nome", Description = "Retorna lista de peças que contenham parte do nome buscado.")]
        public async Task<IActionResult> GetPlayPorNome(string? nome)
        {
            var pecas = await _context.Play.Where(u => u.Name.Contains(nome)).ToListAsync();
            if (pecas.IsNullOrEmpty())
            {
                return NotFound();
            }
            return new ObjectResult(pecas);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra peça", Description = "Realiza cadastro de uma nova peça.")]
        public async Task<IActionResult> PostPlay(Play peca)
        {
            _context.Play.Add(peca);
            await _context.SaveChangesAsync();
            return Ok("Peça cadastrada com sucesso!");
        }

        [HttpPut, Authorize]
        [SwaggerOperation(Summary = "Altera peça", Description = "Altera cadastro de um peça.")]
        public async Task<IActionResult> PutPlay(Play peca)
        {
            _context.Play.Update(peca);
            await _context.SaveChangesAsync();
            return Ok("Peça alterada com sucesso!");
        }

        [HttpDelete, Authorize, Route("{nome}")]
        [SwaggerOperation(Summary = "Deleta peça", Description = "Deleta cadastro de um peça.")]
        public async Task<IActionResult> DeletePlay(string? nome)
        {
            var peca = await _context.Play.FirstOrDefaultAsync(u => u.Name == nome);
            if (peca == null)
                return NotFound();
            _context.Play.Remove(peca);
            await _context.SaveChangesAsync();
            return Ok("Peça deletada com sucesso");
        }
    }
}
