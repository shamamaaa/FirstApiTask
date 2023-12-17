using System;
using System.Linq.Expressions;

namespace API_AB104_.Services.Implementations
{
	public class CategoryService:ICategoryService
	{
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }


        public async Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip:(page - 1)*take,take:take,IsTracking: false).ToListAsync();

            ICollection<GetCategoryDto> categoryDtos = new List<GetCategoryDto>();
            foreach (Category category in categories)
            {
                categoryDtos.Add(new GetCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return categoryDtos;
        }

        public async Task<GetCategoryDto> GetByIdAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Not found");
            return new GetCategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task CreateAsync(CreateCategoryDto createCategoryDto)
        {
            await _repository.AddAsync(new Category
            {
                Name = createCategoryDto.Name
            });

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category == null) throw new Exception("Not Found");

            category.Name = updateCategoryDto.Name;

            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category == null) throw new Exception("Not found");

            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }

    }
}

