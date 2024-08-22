namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IPlayRepository
    {
        Domain.Entity.Play? GetPlay(string name);
        void CreatePlay(Domain.Entity.Play play);
        void UpdatePlay(Domain.Entity.Play play);
        void DeletePlay(string name);
        IEnumerable<Domain.Entity.Play> GetAllPlays();
    }
}