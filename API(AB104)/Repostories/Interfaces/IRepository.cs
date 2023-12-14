using System;
using System.Linq.Expressions;

namespace API_AB104_.Repostories.Interfaces
{
    public interface IRepository
    {
        Task<IQueryable<Category>> GetAllAsync(Expression<Func<Category, bool>>? expression = null, params string[] includes);
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task SaveChangesAsync();
    }
}

