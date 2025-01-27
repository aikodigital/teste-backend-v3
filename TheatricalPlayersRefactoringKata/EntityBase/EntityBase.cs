using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Exception;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repository;

namespace Sistema.microsservice.Domain.EntidadeBase
{
    public class EntityBase<T>
    {
        private readonly ICrudGenerico<T> _crudGenerico;

        public EntityBase(ICrudGenerico<T> crudGenerico)
        {
            _crudGenerico = crudGenerico;
        }

        public virtual async Task AddAsync(T entidade)
        {
            try
            {
                await _crudGenerico.Add(entidade);

            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<long> AddReturnByIdAsync(T entidade)
        {
            try
            {
                return await _crudGenerico.AddReturnById(entidade);

            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task DeleteAsync(T entidade)
        {
            try
            {
                await _crudGenerico.Delete(entidade);

            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public virtual async Task UpdateAsync(T entidade)
        {
            try
            {
                await _crudGenerico.Update(entidade);

            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _crudGenerico.GetAll();

            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<T?> GetByIdAsync(long id)
        {
            try
            {
                return await _crudGenerico.GetById(id);
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public virtual async Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>>? filtro = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                return await _crudGenerico.GetByFilter(filtro, includes);
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message);
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
