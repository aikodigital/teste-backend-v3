using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.Models;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IPlayService
    {
        public Task<ActionResult> Create(PlayModel play);
        public Task<ActionResult> GetByName(string name);
        public Task<ActionResult> GetByPlayId(string playId);
        public Task<ActionResult> Update(PlayModel playModel);
        public Task<ActionResult> Delete(string id);
        public Task<ActionResult> GetAll();
    }
}
