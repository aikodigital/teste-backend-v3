using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.UseCases.StatementUseCase
{
    public interface IStatementItemUseCase
    {
        List<Item> GetAll();
        List<Item> GetAllStatementItemByIdStatement(int id);
    }
}
