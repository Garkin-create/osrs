using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OSRS.Domain.Entities;

namespace OSRS.Domain.Seed
{
    public interface IRepository<T, in Tkey> where T : class, IEntity<Tkey>
    {
        IQueryable<T> GetAll();     
        Task<T> Get(Tkey id);
        Task<bool> AddAsync(T entity);
        Task<bool> AddRange(IEnumerable<T> entities);
        Task<bool> AddAndDeleteRange(IEnumerable<T> delete, IEnumerable<T> entities);
        Task<T> Update(T entity);
        Task<T> Remove(Tkey id);
        Task<T> Remove(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task<bool> UpdateAsync(T entity);
        Task<T> GetSingleBySpecAsync(ISpecification<T> spec);


    }
}
