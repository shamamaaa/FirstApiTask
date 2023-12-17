using System;
using API_AB104_.Repostories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace API_AB104_.Repostories.Implementations
{
    public class Repository <T>: IRepository<T> where T : BaseEntity, new()
    {
        private readonly DbSet<T> _table;
        private readonly AppDbContext _context;


        public Repository(AppDbContext context)
        {
            _table = context.Set<T>();
            _context = context;
        }


        public IQueryable<T> GetAllAsync(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null,
            bool IsDescending = false,
            int skip = 0,
            int take = 0,
            bool IsTracking = true,
            params string[] includes)
        {
            var query = _table.AsQueryable();
            if (expression is not null)query = query.Where(expression);

            if (orderExpression is not null)
            {
                if (IsDescending) query = query.OrderByDescending(orderExpression);
                else query = query.OrderBy(orderExpression);
            }

            if (skip != 0) query = query.Skip(skip);

            if (take != 0) query = query.Take(take);

            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return IsTracking ? query : query.AsNoTracking();        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity= await _table.FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }
    }
}

