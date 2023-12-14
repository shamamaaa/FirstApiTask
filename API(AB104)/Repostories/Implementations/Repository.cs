using System;
using API_AB104_.Repostories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace API_AB104_.Repostories.Implementations
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IQueryable<Category>> GetAllAsync(Expression<Func<Category, bool>>? expression = null, params string[] includes)
        {
            var query = _context.Categories.AsQueryable();
            if (expression is not null)
            {
                query = query.Where(expression);
            }
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            Category category= await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}

