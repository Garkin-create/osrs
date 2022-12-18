using OSRS.Domain.Entities;
using OSRS.Domain.IRepositories;
using OSRS.Persistance.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OSRS.Domain;

namespace OSRS.Persistance.Repositories
{
    public class EfReadOnlyRepository<TEntity> : IReanOnlyRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly OSRSReadOnlyDbContext DbContext;

        public DbSet<TEntity> Entities { get; }

        public virtual IQueryable<TEntity> Table => Entities;

        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public EfReadOnlyRepository(OSRSReadOnlyDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
        }

        public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return Entities.FindAsync(ids, cancellationToken);
        }

        public virtual TEntity GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }
        
        public async Task<TEntity> GetSingleBySpecAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            try
            {
                return await Task.FromResult(List(spec).FirstOrDefault());
            }
            catch (Exception exc)
            {
                return null;
            }
        }
        
        public IQueryable<TEntity> List(ISpecification<TEntity> spec, bool expandable = true)
        {
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(Entities.AsNoTracking().AsQueryable(),
                    (current, include) => current.Include(include));

            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return expandable ? secondaryResult.AsEx.AsExpandable().Where(spec.Criteria).AsQueryable() :
            // secondaryResult.Where(spec.Criteria).AsQueryable();
            
            return secondaryResult.Where(spec.Criteria).AsQueryable();

        }
    }
}