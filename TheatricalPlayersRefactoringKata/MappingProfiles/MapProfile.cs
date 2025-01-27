using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Domain.MappingProfiles
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<InvoiceEntity, Invoice>().ReverseMap();
            CreateMap<PerformanceEntity, Performance>().ReverseMap();
            CreateMap<PlayEntity, Play>().ReverseMap();
        }
    }
}
