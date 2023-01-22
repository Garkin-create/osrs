using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using OSRS.Domain.Entities;
using OSRS.Domain.Seed;
using LinqKit;

namespace OSRS.Infrastructure.Repositories
{
    public abstract class Repository<T,TKey> : IRepository<T, TKey> where T : class, IEntity<TKey>
    {
        protected readonly DbSet<T> DataSet;
        protected readonly DomainContext _context;
        protected readonly IMapper _mapper;
        public Repository(DomainContext context, IMapper mapper = null)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            DataSet = context.Set<T>();
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                return await DataSet.FindAsync(new object[] { id });
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        //public async Task<bool> AddAsync(T entity)
        //{
        //    bool answer = false;
        //    try
        //    {                
        //        using (var context = new Context())
        //        {
        //            context.Set<T>().Add(entity);
        //            await context.SaveChangesAsync();
        //        }                
        //        answer = true;
        //    }
        //    catch (Exception e)
        //    {
        //        answer = false;
        //    }
            
        //    return answer;
        //}

        public async Task<bool> AddRange(IEnumerable<T> entities)
        {
            bool answer = false;
            try
            {
                using (var context = new DomainContext())
                {
                    //var rowsToDelete = GetAll().
                    context.Set<T>().AddRange(entities);
                    await context.SaveChangesAsync();
                }
                answer = true;
            }
            catch (Exception e)
            {
                answer = false;
            }

            return answer;
        }

        public async Task<bool> AddAndDeleteRange(IEnumerable<T> delete, IEnumerable<T> entities)
        {
            bool answer = false;
            try
            {
                using (var context = new DomainContext())
                {
                    context.Set<T>().RemoveRange(delete);
                    context.Set<T>().AddRange(entities);
                    await context.SaveChangesAsync();
                }
                answer = true;
            }
            catch (Exception e)
            {
                answer = false;
            }

            return answer;
        }
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await DataSet.Where(expression).ToListAsync();
        }
     
        public async Task<T> Get(TKey id)
        {
            return await DataSet.FindAsync(id);
        }
      
        public IQueryable<T> GetAll()
        {
            return DataSet;
        }
        public async Task<T> Remove(TKey id)
        {
            var entity = await DataSet.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            DataSet.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Remove(T entity)
        {
            DataSet.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            var answer = false;
            try
            {
                var result = DataSet.Update(entity);
                await _context.SaveChangesAsync();
                answer = true;
            }
            catch (Exception exc)
            {
                answer = false;
            }

            return answer;
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            var answer = false;
            try
            {
                var result = await DataSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                entity = result.Entity;
                answer = true;
            }
            catch (Exception exc)
            {
                answer = false;
            }

            return answer;
        }

        public async Task<T> GetSingleBySpecAsync(ISpecification<T> spec)
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

        public IQueryable<T> List<T>(ISpecification<T> spec, bool expandable = true) where T : class
        {
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_context.Set<T>().AsNoTracking().AsQueryable(),
                    (current, include) => current.Include(include));

            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            return expandable ? secondaryResult.AsExpandable().Where(spec.Criteria).AsQueryable() :
                secondaryResult.Where(spec.Criteria).AsQueryable();

        }
    }
}
