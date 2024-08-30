using JogadoresTeatrais.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JogadoresTeatrais.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaService faturaService;

        public FaturaController (IFaturaService faturaService)
            {
            this.faturaService = faturaService;
            }

        [HttpGet]
        public IActionResult Get()
        {
            this.faturaService.Teste();
            return Ok("Ok");
        }

    }
}
