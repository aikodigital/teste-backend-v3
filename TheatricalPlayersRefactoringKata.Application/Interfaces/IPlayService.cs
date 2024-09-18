using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.Models;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IPlayService
    {
        public Task<ActionResult> Create(PlayModel play);
        public Task<ActionResult> GetByName(string name);
        public Task<ActionResult> Update(PlayModel playModel, string id);
        public Task<ActionResult> Delete(string id);
        public Task<ActionResult> GetAll();
    }
}
