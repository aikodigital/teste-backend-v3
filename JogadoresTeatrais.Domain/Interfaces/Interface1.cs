﻿using JogadoresTeatrais.Domain.Entities;
using JogaresTeatrais.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogadoresTeatrais.Domain.Interfaces
{
    public interface IJogarRepository : IRepository<Jogar>
    { 
        IEnumerable<Jogar> GetAll();
    }
}
