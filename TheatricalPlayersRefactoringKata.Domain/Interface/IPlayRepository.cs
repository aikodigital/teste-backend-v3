using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interface
{
    public interface IPlayRepository
    {
        public Task<bool> Create(Play play);
        public Task<Play> GetByName(string name);
        public Task<Play> GetByPlayId(string playId);
        public Task<bool> Delete(string id);
        public Task<Play> Update(Play play);
        public Task<IEnumerable<Play>> GetAll();
    }
}
