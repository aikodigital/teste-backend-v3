using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.UseCases.StatementUseCase
{
    public interface IConvertUseCase
    {
        string ConvertJsonToXml(string json);
        string ConvertJsonToTxt(string json);
    }
}
