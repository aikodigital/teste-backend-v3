using JogadoresTeatrais.Domain.Interfaces;
using JogaresTeatrais.Data.Context;


namespace JogaresTeatrais.Data.Repositories
{
    public class JogarRepository : Repository<Jogar>, IJogarRepository
    {
        public JogarRepository(DataContext context) : base(context) { }

        public IEnumerable<Jogar> GetAll()
        {
            return Query(x => x.Id > 0);
        }
    }
}
