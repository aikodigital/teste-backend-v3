using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.UseCases.StatementUseCase
{
    public interface IStatementUseCase
    {
        void CreateStatement(Statement statement);
        void UpdateStatement(Statement statement);
        void DeleteStatement(Statement statement);
        Statement GetByIdStatement(int id);
        List<Statement> GetAllStatement();
    }
}
