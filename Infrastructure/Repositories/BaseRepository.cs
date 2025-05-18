using Base_API.Entities;
using Base_API.Infrastructure.Context;
using Base_API.Infrastructure.Exceptions;
using Base_API.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Base_API.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity>(MainDbContext db) : IBaseRepository<TEntity>, IDisposable where TEntity : EntityBase
    {
        private readonly string entityName = typeof(TEntity).Name.ToLower();

        private readonly MainDbContext _db = db;
        private protected readonly DbSet<TEntity> _dbSet = db.Set<TEntity>();
        
        public async Task AddAsync(TEntity entity)
        {
            entity.SetInsertedNow();

            await _db.AddAsync(entity);
        }

        public async Task AddRangeAsync(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                entity.SetInsertedNow();
            }

            await _db.AddRangeAsync(entities);
        }

        public async ValueTask<TEntity?> GetByIdAsync(uint id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async ValueTask<TEntity> GetByIdThrowsIfNullAsync(uint id, string? message = null)
        {
            message ??= $"No {entityName} with ID {id} could be found.";

            return await _dbSet.FindAsync(id) ?? throw new BusinessException(message, HttpStatusCode.NotFound);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
