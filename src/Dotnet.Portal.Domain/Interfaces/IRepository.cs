using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dotnet.Portal.Domain.Models;

namespace Dotnet.Portal.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task CreateEntity(TEntity entity);
        Task<TEntity> GetEntityById(Guid id);
        Task<List<TEntity>> GetEntities();
        Task UpdateEntity(TEntity entity);
        Task DeleteEntity(Guid id);
        Task<IEnumerable<TEntity>> GetEntity(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
