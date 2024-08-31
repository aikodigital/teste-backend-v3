using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace JogadoresTeatrais.Application.Interfaces
{
    public interface IFaturaService
    {
        string GetAll(string formator = "Json");

    }
}
