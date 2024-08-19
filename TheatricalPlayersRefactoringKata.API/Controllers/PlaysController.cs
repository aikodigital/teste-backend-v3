using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheatricalPlayersRefactoringKata.Application.DTOs.PlayDTOs;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaysController : ControllerBase
    {
        private readonly IPlayService _playService;

        public PlaysController(IPlayService playService)
        {
            _playService = playService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlayRequest playRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _playService.CreatePlay(playRequest);

            return response.Status == HttpStatusCode.OK
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _playService.GetPlays();

            return response.Status == HttpStatusCode.OK
                ? Ok(response)
                : BadRequest(response);
        }
    }
}