using Core.Contracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boundaries.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>  where T :class , new()
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _set;
        public BaseRepository(FristHomeworkDbContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }
        public IOperationResult<T> Create(T entity)
        {
            _context.Add(entity);
            return BasicOperationResult<T>.Ok();
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _set.AsQueryable();

            foreach (Expression<Func<T, object>> include in includes)
            {
                queryable = queryable.Include(include);
            }

            return queryable.AnyAsync(condition);
        }

        public Task<List<T>> FindAllAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _set.AsQueryable();

            foreach (Expression<Func<T, object>> include in includes)
            {
                queryable = queryable.Include(include);
            }

            return queryable.Where(condition).ToListAsync();
        }

        public Task<T> FindAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _set.AsQueryable();

            foreach (Expression<Func<T, object>> include in includes)
            {
                queryable = queryable.Include(include);
            }

            return queryable.FirstOrDefaultAsync(condition);
        }

        public IOperationResult<T> Remove(T entity)
        {
            _context.Remove(entity);
            return BasicOperationResult<T>.Ok();
        }

        public Task SaveAsync()
        {
          return _context.SaveChangesAsync();
        }

        public IOperationResult<T> Update(T entity)
        {
            EntityEntry entityEntry = _context.Entry(entity);
            entityEntry.State = EntityState.Modified;

            return BasicOperationResult<T>.Ok();
        }
    }
}
