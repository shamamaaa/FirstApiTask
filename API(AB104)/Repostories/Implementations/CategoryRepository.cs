using System;
using Microsoft.EntityFrameworkCore;

namespace API_AB104_.Repostories.Implementations
{
	public class CategoryRepository : Repository<Category>,ICategoryRepository
	{
        public CategoryRepository(AppDbContext context):base(context)
        {
            context.Categories.OrderBy(c => c.Id);
        }
    }
}

