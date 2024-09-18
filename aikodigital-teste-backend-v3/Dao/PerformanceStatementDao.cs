using aikodigital_teste_backend_v3.Data;
using aikodigital_teste_backend_v3.Dto;
using aikodigital_teste_backend_v3.Models;

namespace aikodigital_teste_backend_v3.Dao
{
    public class PerformanceStatementDao
    {
        private readonly ApplicationDbContext appDbContext;

        public PerformanceStatementDao(ApplicationDbContext _context)
        {
            this.appDbContext = _context;
        }

        public IQueryable<PerformanceStatement> getPerformanceStatement(PerformanceStatementDto performanceStatementDto)
        {
            IQueryable<PerformanceStatement> results = appDbContext.PerformanceStatement.Where(x => x.Customer == performanceStatementDto.Customer && x.PlayId == performanceStatementDto.PlayId);

            return results;
        }
    }
}
