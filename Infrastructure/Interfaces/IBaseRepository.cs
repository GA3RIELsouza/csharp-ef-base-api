using Base_API.Entities;

namespace Base_API.Infrastructure.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : EntityBase
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(params TEntity[] entities);
        ValueTask<TEntity?> GetByIdAsync(uint id);
        ValueTask<TEntity> GetByIdThrowsIfNullAsync(uint id, string? message = null);
        void Update(TEntity entity);
        void UpdateRange(params TEntity[] entities);
        void Remove(TEntity entity);
        void RemoveRange(params TEntity[] entities);
    }
}
