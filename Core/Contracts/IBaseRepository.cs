using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> FindAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes);

        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes);

        void Create(T entity);

        void Update(T entity);

        void Remove(T entity);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes);

        Task SaveAsync();
    }
}
