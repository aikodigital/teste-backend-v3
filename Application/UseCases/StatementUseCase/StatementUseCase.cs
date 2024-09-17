using Domain.Contracts.Repositories;
using Domain.Contracts.UseCases.StatementUseCase;
using Domain.Entities;

namespace Application.UseCases.StatementUseCase
{
    public class StatementUseCase : IStatementUseCase
    {
        private readonly IStatementRepository _statementRepository;

        public StatementUseCase(IStatementRepository statementRepository)
        {
            _statementRepository = statementRepository;
        }

        public void CreateStatement(Statement statement)
        {
            _statementRepository.Create(statement);
        }
        public void UpdateStatement(Statement statement)
        {
            _statementRepository.Update(statement);
        }
        public void DeleteStatement(Statement statement)
        {
            _statementRepository.Delete(statement);
        }
        public Statement GetByIdStatement(int id)
        {
            return _statementRepository.GetById(id);
        }
        public List<Statement> GetAllStatement()
        {
            return _statementRepository.GetAll();
        }
    }
}
