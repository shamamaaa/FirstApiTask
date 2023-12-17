using System;
namespace API_AB104_.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }


        public async Task<ICollection<GetTagDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<GetTagDto> tagDtos = new List<GetTagDto>();
            foreach (Tag tag in tags)
            {
                tagDtos.Add(new GetTagDto
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }

            return tagDtos;
        }

        public async Task<GetTagDto> GetByIdAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag == null) throw new Exception("Not found");
            return new GetTagDto
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public async Task CreateAsync(CreateTagDto createTagDto)
        {
            await _repository.AddAsync(new Tag
            {
                Name = createTagDto.Name
            });

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateTagDto updateTagDto)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag == null) throw new Exception("Not Found");

            tag.Name = updateTagDto.Name;

            _repository.Update(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag == null) throw new Exception("Not found");

            _repository.Delete(tag);
            await _repository.SaveChangesAsync();
        }

    }
}

