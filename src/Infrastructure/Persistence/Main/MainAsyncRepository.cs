using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Persistence.Main;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Main
{
    public class MainAsyncRepository<TEntity> : IMainAsyncRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IDateTime _dateTime;

        public MainAsyncRepository(ApplicationDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _dateTime = dateTime;
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _dbSet.ToListAsync(cancellationToken);

        public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, 
            params Expression<Func<TEntity, object>>[] includeProperties) 
        {
            if (includeProperties is null)
                throw new ArgumentNullException(nameof(includeProperties));
            
            IQueryable<TEntity> query = _dbSet;

            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(object id) =>
            await _dbSet.FindAsync(id);

        public async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter, 
            CancellationToken cancellationToken = default) =>
            await _dbSet.Where(filter).ToListAsync(cancellationToken);

        public Task RemoveAsync(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);

            return Task.CompletedTask;
        }

        public async Task RemoveByIdAsync(object id)
        {
            var entitiy = await GetByIdAsync(id);

            await RemoveAsync(entitiy);
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default) 
        {
            foreach (var entry in _context.ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public Task UpdateAsync(TEntity entity) 
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, 
            CancellationToken cancellationToken = default) =>
            await _dbSet.AnyAsync(filter, cancellationToken);
    }
}