using Aplication.DTO;
using AutoMapper;
using CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entity;

namespace Aplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Play, PlayDto>()
                .ForMember(dest => dest.Type, 
                opt => opt.MapFrom(
                    src => (PlayType)Enum.Parse(typeof(PlayType), src.Type)));
        }
    }
}
