using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dotnet.Portal.Domain.Interfaces;
using Dotnet.Portal.Domain.Models;
using Dotnet.Portal.Data.Context;

namespace Dotnet.Portal.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly DotnetPortalDB Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DotnetPortalDB db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetEntity(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetEntities()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetEntityById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task CreateEntity(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task UpdateEntity(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task DeleteEntity(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
