using Application.ModelViews;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ITheaterPlayService
    {

        public Task<TheaterPlayDTO> create(TheaterPlayModelView theaterPlay);
    }
}
