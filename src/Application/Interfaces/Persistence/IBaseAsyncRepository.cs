using System.Collections.Generic;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces.Persistence
{
    public interface IBaseAsyncRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> GetByIdAsync(object id);

        Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity);

        Task RemoveAsync(TEntity entity);

        Task RemoveByIdAsync(object id);
    }
}