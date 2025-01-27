using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sistema.microsservice.Domain.Configuracao.Entities.Interfaces;
using System.Linq.Expressions;
using TheatricalPlayersRefactoringKata.Domain.Exception;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repository;
using TheatricalPlayersRefactoringKata.Infra.Contexto;

namespace Sistema.microsservice.Infra.Configuracao.Repositories.Generic
{
    public class CrudGenerico<T> : ICrudGenerico<T> where T : class, IEntity
    {
        private readonly AppDbContext _context;
        private readonly ILogger<T> _logger;

        public CrudGenerico(AppDbContext context, ILogger<T> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Add(T entidade)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Add)} - Ínicio da execução - Buscando existência de entidades trackeadas");
                var existingEntity = _context.ChangeTracker.Entries<T>()
                                                           .FirstOrDefault(e => e.Entity.Id.Equals(entidade.Id));

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Add)} - Ínicio da execução - Verificando entidades traqueadas diferente de nulo");
                if (existingEntity != null)
                    _context.Entry(existingEntity.Entity).State = EntityState.Detached;

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Add)} - Ínicio da execução - Verificando entidades traqueadas igual a nulo");
                if (existingEntity == null)
                    await _context.Set<T>().AddAsync(entidade);
                else
                    _context.Set<T>().Update(entidade);

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Add)} - Ínicio da execução - Adicionando datas e status ao registro");
                entidade.DataRegistro = DateTime.Now.ToUniversalTime();
                entidade.DataAtualizacao = DateTime.Now.ToUniversalTime();
                entidade.StatusRegistro = entidade.StatusRegistro ?? true;

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Add)} - Fim da execução - Salvando registro");
                await _context.SaveChangesAsync();
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception($"Erro ao salvar: {ex.Message}");
            }
        }

        public async Task<long> AddReturnById(T entidade)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Add)} - Ínicio da execução - Buscando existência de entidades trackeadas");
                var existingEntity = _context.ChangeTracker.Entries<T>()
                                                           .FirstOrDefault(e => e.Entity.Id.Equals(entidade.Id));

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Add)} - Ínicio da execução - Verificando entidades traqueadas diferente de nulo");
                if (existingEntity != null)
                    _context.Entry(existingEntity.Entity).State = EntityState.Detached;

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Add)} - Ínicio da execução - Verificando entidades traqueadas igual a nulo");
                if (existingEntity == null)
                    await _context.Set<T>().AddAsync(entidade);
                else
                    _context.Set<T>().Update(entidade);

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Add)} - Ínicio da execução - Adicionando datas e status ao registro");
                entidade.DataRegistro = DateTime.Now.ToUniversalTime();
                entidade.DataAtualizacao = DateTime.Now.ToUniversalTime();
                entidade.StatusRegistro = entidade.StatusRegistro ?? true;

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Add)} - Fim da execução - Salvando registro");
                await _context.SaveChangesAsync();

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Add)} - Fim da execução - Retornando o ID do registro salvo");
                return entidade.Id;
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception($"Erro ao salvar: {ex.Message}");
            }
        }

        public async Task Update(T entidade)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Update)} - Ínicio da execução - Buscando se existe trackeamento na entidade passada");
                var existingEntityEntry = _context.ChangeTracker.Entries<T>()
                                                                .FirstOrDefault(e => ((dynamic)e.Entity).Id == ((dynamic)entidade).Id);

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Update)} - Ínicio da execução - Validando se a entidade é diferente de null");
                if (existingEntityEntry != null)
                    _context.Entry(existingEntityEntry.Entity).State = EntityState.Detached;

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Update)} - Ínicio da execução - Adicionando data de atualização no registro");
                entidade.DataAtualizacao = DateTime.Now.ToUniversalTime();
                _context.Set<T>().Update(entidade);

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Update)} - Fim da execução - Salvando registro");
                await _context.SaveChangesAsync();
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception($"Erro ao salvar: {ex.Message}");
            }
        }


        public async Task Delete(T entidade)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Delete)} - Ínicio da execução - Buscando se existe trackeamento na entidade passada");
                var existingEntity = _context.Set<T>().Local.FirstOrDefault(e => e.Id == entidade.Id);

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Delete)} - Ínicio da execução - Validando se a entidade é diferente de null");
                if (existingEntity != null)
                    _context.Entry(existingEntity).State = EntityState.Detached;

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Delete)} - Ínicio da execução - Atachando entidade");
                _context.Set<T>().Attach(entidade);

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Delete)} - Ínicio da execução - Removendo entidade");
                _context.Set<T>().Remove(entidade);

                _logger.Log(LogLevel.Information, $"Entidade: {nameof(T)} - Ação: {nameof(Delete)} - Fim da execução - Salvando alterações");
                await _context.SaveChangesAsync();
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception($"Erro ao salvar: {ex.Message}");
            }
        }
        public async Task<List<T>> GetAll()
        {
            try
            {
                return await _context.Set<T>().AsNoTracking().ToListAsync();
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception($"Erro ao salvar: {ex.Message}");
            }
        }

        public async Task<List<T>> GetByFilter(Expression<Func<T, bool>>? filtro = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var include in includes)
                    query = query.AsNoTracking().Include(include);

                if (filtro != null)
                    query = query.AsNoTracking().Where(filtro);

                return await query.ToListAsync();
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception($"Erro ao salvar: {ex.Message}");
            }
        }

        public async Task<T?> GetById(long id)
        {
            try
            {
                var retorno = (await _context.Set<T>().AsNoTracking().Where(b => b.Id == id).ToListAsync()).FirstOrDefault();
                return retorno;
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception($"Erro ao salvar: {ex.Message}");
            }
        }
    }

}
