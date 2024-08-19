using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Server.Database.DTOs.Play;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.Play;
using TheatricalPlayersRefactoringKata.Server.DTOs.Play;

namespace TheatricalPlayersRefactoringKata.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayController : ControllerBase
    {
        private readonly PlayRepository _repository;
        private readonly IMapper _mapper;

        public PlayController(PlayRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria uma nova peça de teatro com o nome e tipo especificados.
        /// </summary>
        /// <param name="request">CreatePlayRequest com o nome e tipo da peça de teatro.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatePlayResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePlayRequest request)
        {
            if (await _repository.GetByTitle(request.Play.Name) != null)
            {
                return BadRequest("Play already exists");
            }

            if (AbstractPlayType.FromString(request.Play.Type) == null)
            {
                return BadRequest("Invalid play type");
            }

            // Map to entity and insert to database
            await _repository.Insert(_mapper.Map<PlayEntity>(request.Play));

            return CreatedAtAction(nameof(Get), new { name = request.Play.Name }, new CreatePlayResponse { });
        }

        /// <summary>
        /// Deleta uma peça de teatro com o nome especificado.
        /// </summary>
        /// <param name="name">Nome da peça de teatro a ser deletada.</param>
        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(string name)
        {
            PlayEntity? play = await _repository.GetByTitle(name);
            if (play == null)
            {
                return NotFound();
            }

            await _repository.Delete(play);

            return NoContent();
        }

        /// <summary>
        /// Retorna uma peça de teatro com o nome especificado.
        /// </summary>
        /// <param name="name">Nome da peça de teatro a ser retornada.</param>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayEntity))]
        public async Task<IActionResult> Get(string name)
        {
            PlayEntity? play = await _repository.GetByTitle(name);
            if (play == null)
            {
                return NotFound();
            }

            return Ok(play);
        }

        /// <summary>
        /// Retorna todas as peças de teatro.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlayEntity>))]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<PlayEntity> plays = await _repository.GetAll();
            return Ok(plays);
        }
    }
}
