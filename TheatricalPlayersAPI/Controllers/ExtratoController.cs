using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

using TheatricalPlayersRefactoringKata.API.Data;
using TheatricalPlayersRefactoringKata.ViewModels;
using Microsoft.IdentityModel.Tokens;

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

    }
}
