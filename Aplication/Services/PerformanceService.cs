using Aplication.DTO;
using Aplication.Services.Interfaces;
using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly TesteBackendV3DbContext _context;
        private readonly IMapper _mapper;

        public PerformanceService(TesteBackendV3DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<PerformanceDto> GetPerformances()
        {
            var performances = _context.Performances
                .Include(x=> x.Play)
                .ToList();
            var performancesDto = _mapper.Map<List<PerformanceDto>>(performances);
            return performancesDto;
        }
    }
}
