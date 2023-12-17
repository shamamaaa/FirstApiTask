using System;
namespace API_AB104_.Repostories.Implementations
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {
            context.Tags.OrderBy(c => c.Id);
        }
    }
}

