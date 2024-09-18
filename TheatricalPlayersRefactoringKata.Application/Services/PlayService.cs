using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interface;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class PlayService : IPlayService
    {
        private readonly IPlayRepository _playRepository;

        public PlayService(IPlayRepository playRepository)
        {
            _playRepository = playRepository;
        }

        public async Task<ActionResult> Create(PlayModel play)
        {
            try
            {
                var entity = PlayModel.ConvertToEntity(play);

                var result = await _playRepository.Create(entity);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var result = await _playRepository.Delete(id);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        public async Task<ActionResult> GetByName(string name)
        {
            try
            {
                var result = await _playRepository.GetByName(name);

                var model = PlayModel.ConvertToModel(result);

                return new OkObjectResult(model);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result = await _playRepository.GetAll();

                var model = PlayModel.ConvertToModels(result);

                return new OkObjectResult(model);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        public async Task<ActionResult> Update(PlayModel play, string id)
        {
            try
            {
                var entity = PlayModel.ConvertToEntity(play);
                entity.Id = id;

                var result = await _playRepository.Update(entity, id);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
