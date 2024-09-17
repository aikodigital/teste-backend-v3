using Domain.Contracts.Repositories;
using Domain.Contracts.UseCases.StatementUseCase;
using Domain.Entities;

namespace Application.UseCases.StatementUseCase
{
    public class StatementItemUseCase : IStatementItemUseCase
    {
        private readonly IStatementItemRepository _statementItemRepository;

        public StatementItemUseCase(IStatementItemRepository statementItemRepository)
        {
            _statementItemRepository = statementItemRepository;
        }

        public List<Item> GetAll()
        {
            return _statementItemRepository.GetAll();
        }

        public List<Item> GetAllStatementItemByIdStatement(int id)
        {
            List<Item> itens = _statementItemRepository.GetAll().Where(s => s.StatementId == id).ToList();

            return itens;
        }
    }
}
