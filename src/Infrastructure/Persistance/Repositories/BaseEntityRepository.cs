using OSRS.Domain.Entities;
using OSRS.Persistance.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using OSRS.Domain;
using OSRS.Persistance.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace OSRS.Persistance.Repositories
{
    public abstract class BaseEntityRepository
    {
        protected readonly ISystemLogger _logger;

        protected BaseEntityRepository(ISystemLogger logger)
        {
            _logger = logger;
        }

        public static IServiceCollection Register(IServiceCollection services, Assembly assembly = null)
        {
            if (assembly == null)
                assembly = typeof(BaseEntityRepository).Assembly;
            foreach (var c in assembly.GetTypes().Where(p => p.GetInterface(typeof(IEntityRepository<>).Name) != null))
            {
                Type interfaceType;
                Type implementationType;
                if (c.IsInterface)
                {
                    interfaceType = c;
                    implementationType = assembly
                        .GetTypes()
                        .FirstOrDefault(p => p.GetInterface(c.Name) != null && p.IsClass);
                }
                else
                {
                    implementationType = c;
                    CustomAttributeData attr;
                    if ((attr = c.CustomAttributes
                            .FirstOrDefault(p => p.AttributeType == typeof(AutoRegister))) != null)
                        interfaceType = (Type)attr.ConstructorArguments[0].Value;
                    else
                        interfaceType = c.GetInterface(typeof(IEntityRepository<>).Name);
                }

                services.AddCommonImplementationService(interfaceType, implementationType);
            }

            return services;
        }
    }
    public class BaseEntityRepository<TEntity> : BaseEntityRepository where TEntity : class, IEntity
    {
        protected readonly DbContext _context;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
        protected readonly DbSet<TEntity> DataSet;
        
        
        public BaseEntityRepository(DbContext dbContext, ISystemLogger logger): base(logger)
        {
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            Entities = _context.Set<TEntity>();
            DataSet = _context.Set<TEntity>();
            _context = dbContext;
            
        }

        

        // protected async Task<IQueryable<T>> GetSourceByUsernameAsync<T>(string username, 
        //     CancellationToken cancellationToken = default) where T : class, IUserRelation
        // {
        //     IQueryable<T> query = null;
        //     try
        //     {
        //         var userId = await _context.Set<UserAccountEntity>().AsNoTracking()
        //             .Where(p => p.UserName == username)
        //             .Select(s => s.Id).FirstOrDefaultAsync(cancellationToken);
        //         query = await GetSourceByUserId<T>(userId, cancellationToken);
        //     }
        //     catch (Exception exc)
        //     {
        //         await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
        //     }
        //     finally
        //     {
        //         query ??= Enumerable.Empty<T>().AsQueryable();
        //     }
        //     return query;
        // }

        // protected async Task<IQueryable<T>> GetSourceByUserId<T>(string userId,
        //     CancellationToken cancellationToken = default) where T : class, IUserRelation
        // {
        //     IQueryable<T> query = null;
        //     try
        //     {
        //         if (!string.IsNullOrEmpty(userId))
        //             query = _context.Set<T>().AsNoTracking().Where(p => p.UserId == userId);
        //     }
        //     catch (Exception exc)
        //     {
        //         await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
        //     }
        //
        //     return query;
        // }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DataSet.FindAsync(new object[] { id }, cancellationToken);
            }
            catch (Exception exc)
            {
                await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
                return null;
            }
        }

        public TEntity GetById(Guid id)
        {
            try
            {
                var task = Task.Run(async () => await GetByIdAsync(id));
                return task.Result;
            }
            catch (Exception exc)
            {
                // _logger.LogException(this, exc);
                return null;
            }
        }

        public TEntity GetSingleBySpec(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            try
            {
                return List(spec).FirstOrDefault();
            }
            catch (Exception exc)
            {
                // _logger.LogException(this, exc);
                return null;
            }
        }

        public async Task<TEntity> GetSingleBySpecAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            try
            {
                return await Task.FromResult(List(spec).FirstOrDefault());
            }
            catch (Exception exc)
            {
                // await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
                return null;
            }
        }
        public async Task<T> GetSingleBySpecAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                return await List(spec).FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception exc)
            {
                //await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
                return null;
            }
        }
        public virtual async Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var answer = false;
            try
            {
                var result = await DataSet.AddAsync(entity, cancellationToken);
                entity = result.Entity;
                answer = true;
            }
            catch (Exception exc)
            {
                // await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return answer;
        }
        
        public virtual async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            var answer = false;
            try
            {
                await DataSet.AddRangeAsync(entities, cancellationToken);
                answer = true;
            }
            catch (Exception exc)
            {
                answer = false;
                await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return answer;
        }

        public virtual bool Add(TEntity entity)
        {
            var task = Task.Run(async () => await AddAsync(entity));
            return task.Result;
        }

        public virtual bool Update(TEntity entity)
        {
            var task = Task.Run(async () => await UpdateAsync(entity));
            return task.Result;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var answer = false;
            try
            {
                var result = DataSet.Update(entity);
                answer = true;
            }
            catch (Exception exc)
            {
                // await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return answer;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, object>>[] properties, CancellationToken cancellationToken = default)
        {
            bool answer = false;
            try
            {
                DataSet.Attach(entity);
                var dbEntityEntry = _context.Entry(entity);
                if (properties.Any())
                {
                    foreach (var property in properties)
                        dbEntityEntry.Property(property).IsModified = true;
                }
                else
                {
                    foreach (var property in dbEntityEntry.OriginalValues.Properties)
                    {
                        string propertyName = property.Name;
                        var original = dbEntityEntry.OriginalValues.GetValue<object>(propertyName);
                        var current = dbEntityEntry.CurrentValues.GetValue<object>(propertyName);
                        if (original != null && !original.Equals(current))
                            dbEntityEntry.Property(propertyName).IsModified = true;
                    }
                }

                answer = true;
            }
            catch (Exception exc)
            {
                // await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return answer;
        }

        public virtual async Task<bool> UpdateAsync<T>(TEntity entity, Expression<Func<TEntity, object>>[] properties, CancellationToken cancellationToken = default) where T : class
        {
            bool answer = false;
            try
            {
                DataSet.Attach(entity);
                var dbEntityEntry = _context.Entry(entity);
                if (properties.Any())
                {
                    foreach (var property in properties)
                        dbEntityEntry.Property(property).IsModified = true;
                }
                else
                {
                    foreach (var property in dbEntityEntry.OriginalValues.Properties)
                    {
                        string propertyName = property.Name;
                        var original = dbEntityEntry.OriginalValues.GetValue<object>(propertyName);
                        var current = dbEntityEntry.CurrentValues.GetValue<object>(propertyName);
                        if (original != null && !original.Equals(current))
                            dbEntityEntry.Property(propertyName).IsModified = true;
                    }
                }

                answer = true;
            }
            catch (Exception exc)
            {
                // await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return answer;
        }


        public virtual async Task<bool> UpdateWhereAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> property, object newValue, CancellationToken cancellationToken = default)
        {
            var answer = false;
            try
            {
                var collection = DataSet.Where(predicate);
                foreach (var item in collection)
                {
                    // if (item is IDomainUpdatedLog uEntity)
                    // {
                    //     uEntity.LastModifiedBy = _userMetadata?.Username ?? "System";
                    //     uEntity.LastModifiedDate = DateTime.Now;
                    // }
                    //
                    // if (item is IDomainEntity)
                    // {
                    //     var dbEntry = _context.Entry(item);
                    //     var propertyName = property.GetPropertyAccess().Name;
                    //     dbEntry.Property(propertyName).CurrentValue = newValue;
                    // }
                    //
                    // if (item is not UserAccountEntity user) continue;
                    // {
                    //     var dbEntry = _context.Entry(item);
                    //     var propertyName = property.GetPropertyAccess().Name;
                    //     dbEntry.Property(propertyName).CurrentValue = newValue;
                    //     user.UpdatedBy = _userMetadata?.Username ?? "System";
                    //     user.UpdatedDate = DateTime.Now;
                    // }
                }

                answer = true;
            }
            catch (Exception exc)
            {
                // await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return answer;
        }

        public virtual async Task<bool> ToggleValueWhereAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> property, CancellationToken cancellationToken = default)
        {
            var answer = false;
            try
            {
                var collection = DataSet.Where(predicate);
                foreach (var item in collection)
                {
                    // if (item is IDomainUpdatedLog uEntity)
                    // {
                    //     uEntity.LastModifiedBy = _userMetadata?.Username ?? "System";
                    //     uEntity.LastModifiedDate = DateTime.Now;
                    // }
                    //
                    // if (item is IDomainEntity dEntity)
                    // {
                    //     var dbEntry = _context.Entry(item);
                    //     var propertyName = property.GetPropertyAccess().Name;
                    //     dbEntry.Property(propertyName).CurrentValue = !(bool)dbEntry.Property(propertyName).CurrentValue;
                    // }
                }

                answer = true;
            }
            catch (Exception exc)
            {
                // await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return answer;
        }

        protected virtual async Task<bool> UpdateDeepAsync(TEntity entity, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigation)
        {
            var answer = false;
            try
            {
                // if (entity is IDomainUpdatedLog uEntity)
                // {
                //     uEntity.LastModifiedBy = _userMetadata?.Username ?? "System";
                //     uEntity.LastModifiedDate = DateTime.Now;
                // }
                // if (entity is IDomainEntity dEntity)
                // {
                //     var dbEntity = await GetByIdAsync(dEntity.Id, cancellationToken);
                //     var dbEntry = _context.Entry(dbEntity);
                //     dbEntry.CurrentValues.SetValues(entity);
                //     foreach (var property in navigation)
                //     {
                //         var propertyName = property.GetPropertyAccess().Name;
                //         var currentNavigation = dbEntry.Collection(propertyName);
                //         var accessor = currentNavigation.Metadata.GetCollectionAccessor();
                //
                //         await currentNavigation.LoadAsync(cancellationToken);
                //         var currentCollection = ((IEnumerable<IDomainEntity>)currentNavigation.CurrentValue).ToDictionary(e => e.Id);
                //         var domainEntities = accessor.GetOrCreate(entity, true) as IEnumerable<IDomainEntity> ?? Array.Empty<IDomainEntity>();
                //
                //         foreach (var item in domainEntities)
                //         {
                //             if (!currentCollection.TryGetValue(item.Id, out var oldItem))
                //                 accessor.Add(dbEntity, item, true);
                //             else
                //             {
                //                 _context.Entry(oldItem).CurrentValues.SetValues(item);
                //                 currentCollection.Remove(item.Id);
                //             }
                //         }
                //         foreach (var oldItem in currentCollection.Values)
                //             accessor.Remove(dbEntity, oldItem);
                //     }
                // }
                answer = true;
            }
            catch (Exception exc)
            {
               //await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }
            return answer;
        }

        public async Task<int> UpdateQueryAsync(string sql, params object[] parameters)
        {
            var answer = 0;
            try
            {
                answer = await _context.Database.ExecuteSqlRawAsync(sql, parameters);
            }
            catch (Exception exc)
            {
              //  _logger.LogException(this, exc);
            }

            return answer;
        }

        public virtual async Task<bool> ReorderWhereAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> property, int fromIndex, int toIndex, CancellationToken cancellationToken = default)
        {
            var answer = false;
            try
            {
                var collection = DataSet.Where(predicate);
                foreach (var item in collection)
                {
                    // if (item is IDomainUpdatedLog uEntity)
                    // {
                    //     uEntity.LastModifiedBy = _userMetadata?.Username ?? "System";
                    //     uEntity.LastModifiedDate = DateTime.Now;
                    // }

                    // if (item is not IDomainEntity dEntity) continue;
                    var dbEntry = _context.Entry(item);
                    // var propertyName = property.GetPropertyAccess().Name;

                    // var currentValue = (int)dbEntry.Property(propertyName).CurrentValue;
                    // if (currentValue == fromIndex)
                    //     dbEntry.Property(propertyName).CurrentValue = toIndex;
                    // else
                    // {
                    //     var newValue = currentValue + ((fromIndex > toIndex) ? 1 : -1);
                    //     dbEntry.Property(propertyName).CurrentValue = newValue;
                    // }
                }

                answer = true;
            }
            catch (Exception exc)
            {
               //await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return answer;
        }

        public bool Delete(TEntity entity)
        {
            var answer = false;
            try
            {
                var result = DataSet.Remove(entity);
                entity = result.Entity;
                answer = true;
            }
            catch (Exception exc)
            {
              //  _logger.LogException(this, exc);
            }

            return answer;
        }

        public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                return await Task.FromResult(Delete(entity));
            }
            catch (Exception exc)
            {
               //await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }
            return false;
        }

        public bool DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            var answer = false;
            try
            {
                var collection = DataSet.Where(predicate);
                foreach (var item in collection)
                {
                    DataSet.Remove(item);
                }

                answer = true;
            }
            catch (Exception exc)
            {
              //  _logger.LogException(this, exc);
            }

            return answer;
        }

        public async Task<bool> DeleteWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            bool answer;
            try
            {
                answer = await Task.FromResult(DeleteWhere(predicate));
            }
            catch (Exception exc)
            {
                answer = false;
               //await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return answer;
        }

        public virtual async Task<bool> BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var deleted = false;
            try
            {
                var collection = DataSet.Where(predicate).Select(s => ((IEntity<long>)s).Id.ToString()).ToList();
                if (collection.Count > 0) {
                     var entityType = _context.Model.FindEntityType(typeof(TEntity));
                     var tableName = entityType.GetTableName();
                     var sql = $@"DELETE FROM {tableName} WHERE Id IN({string.Join(", ", collection.Select(x => $"'{x}'").ToList())});";
                     await _context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
                }

                deleted = true;
            }
            catch (Exception exc)
            {
                deleted = false;
                await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return deleted;
        }

        public bool BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            var task = Task.Run(async () => await BulkDeleteAsync(predicate));
            return task.Result;
        }

        public IQueryable<TEntity> ListAll(string[] includes = null)
        {
            var query = DataSet.AsNoTracking();
            try
            {
                if (includes != null) 
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            catch (Exception exc)
            {
              //  _logger.LogException(this, exc);
            }

            return query; //.AsExpandable();
        }
        public IQueryable<TEntity> ListAll(IQueryable<TEntity> source,string[] includes = null)
        {
            var query = source.AsNoTracking();
            try
            {
                if (includes != null)
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            catch (Exception exc)
            {
              //  _logger.LogException(this, exc);
            }

            return query; //.AsExpandable();
        }
       
        public IQueryable<TEntity> List(IQueryable<TEntity> source, ISpecification<TEntity> spec)
        {
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(source.AsNoTracking().AsQueryable(),
                    (current, include) => current.Include(include));

            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            return secondaryResult.Where(spec.Criteria).AsQueryable();
            //return secondaryResult.AsExpandable().Where(spec.Criteria).AsQueryable();
        }
        
        public IQueryable<TEntity> List(ISpecification<TEntity> spec, bool expandable = true)
        {
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(DataSet.AsNoTracking().AsQueryable(),
                    (current, include) => current.Include(include));

            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            //return expandable ? secondaryResult.AsExpandable().Where(spec.Criteria).AsQueryable() :
            return expandable ? secondaryResult.Where(spec.Criteria).AsQueryable() :
                secondaryResult.Where(spec.Criteria).AsQueryable();

        }
       
        public IQueryable<T> ListAll<T>(IQueryable<T> source ,string[] includes = null) where T : class
        {
            var query = source.AsNoTracking();
            try
            {
                if (includes != null) 
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            catch (Exception exc)
            {
              //  _logger.LogException(this, exc);
            }

            return query; //.AsExpandable();
        }
        
        public IQueryable<T> ListAll<T>(string[] includes = null) where T : class
        {
            return ListAll(_context.Set<T>(), includes);
        }

        public IQueryable<T> List<T>(ISpecification<T> spec, bool expandable = true) where T : class
        { 
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_context.Set<T>().AsNoTracking().AsQueryable(),
                    (current, include) => current.Include(include));

            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return expandable?secondaryResult.AsExpandable().Where(spec.Criteria).AsQueryable():
            return expandable?secondaryResult.Where(spec.Criteria).AsQueryable():
                   secondaryResult.Where(spec.Criteria).AsQueryable();

        }

        public long Max(Expression<Func<TEntity, long>> column, Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> query = DataSet;
            long count = query.Count();
            if (where != null)
            {
                query = query.Where(where);
                count = query.Count();
            }

            if (count > 0)
            {
                return query.Max(column);
            }

            return 0;
        }

        public long Min(Expression<Func<TEntity, long>> column, Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> query = DataSet;
            long count = query.Count();
            if (where != null)
            {
                query = query.Where(where);
                count = query.Count();
            }

            if (count > 0)
            {
                return query.Min(column);
            }

            return 0;
        }

        public IList<T> SqlQuery<T>(string sql, Dictionary<string, object> parameters) where T : class
        {
            var task = Task.Run(async () => await SqlQueryAsync<T>(sql, parameters));
            return task.Result;
        }

        public async Task<IList<T>> SqlQueryAsync<T>(string sql, Dictionary<string, object> parameters, CancellationToken cancellationToken = default) where T : class
        {
            return await _context.SqlQueryAsync<T>(sql, parameters, cancellationToken);
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
        {
            long answer = 0;
            try
            {
                IQueryable<TEntity> query = DataSet;
                if (where != null)
                {
                    query = query.Where(where);
                    answer = query.Count();
                }
                else answer = query.Count();
            }
            catch (Exception exc)
            {
               //await _logger.LogExceptionAsync(this, exc, cancellationToken: cancellationToken);
            }

            return answer;
        }
    }
    
    public interface IEntityRepository<TEntity>
    {
        TEntity GetById(Guid id);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        TEntity GetSingleBySpec(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
        Task<TEntity> GetSingleBySpecAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
        Task<T> GetSingleBySpecAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : class;
        bool Add(TEntity entity);
        Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        bool Update(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, object>>[] properties, CancellationToken cancellationToken = default);
        Task<bool> UpdateWhereAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> property, object newValue, CancellationToken cancellationToken = default);
        Task<bool> ToggleValueWhereAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> property, CancellationToken cancellationToken = default);
        
        Task<int> UpdateQueryAsync(string sql, params object[] parameters);
        
        Task<bool> ReorderWhereAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> property, int fromIndex, int toIndex, CancellationToken cancellationToken = default);
        
        bool DeleteWhere(Expression<Func<TEntity, bool>> predicate);
        Task<bool> DeleteWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        
        bool Delete(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        
        bool BulkDelete(Expression<Func<TEntity, bool>> predicate);
        Task<bool> BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        
        IQueryable<TEntity> ListAll(string[] includes = null);
        IQueryable<TEntity> ListAll(IQueryable<TEntity> source, string[] includes = null);
        IQueryable<TEntity> List(ISpecification<TEntity> spec, bool expandable = true);
        
        IQueryable<T> ListAll<T>(string[] includes = null) where T : class;
        IQueryable<T> List<T>(ISpecification<T> spec, bool expandable=true) where T : class;
        
        long Max(Expression<Func<TEntity, long>> column, Expression<Func<TEntity, bool>> where);
        long Min(Expression<Func<TEntity, long>> column, Expression<Func<TEntity, bool>> where);
        
        IList<T> SqlQuery<T>(string sql, Dictionary<string, object> parameters) where T : class;
        Task<IList<T>> SqlQueryAsync<T>(string sql, Dictionary<string, object> parameters, CancellationToken cancellationToken = default) where T : class;
        Task<long> CountAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
    }
}
