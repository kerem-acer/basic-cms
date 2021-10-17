using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Persistence.Main
{
    public interface IMainAsyncRepository<TEntity> : IBaseAsyncRepository<TEntity> where TEntity: class
    {
        Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter, 
            CancellationToken cancellationToken = default);

        Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, 
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}