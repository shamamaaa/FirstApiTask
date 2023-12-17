using System;
using System.Linq.Expressions;

namespace API_AB104_.Services.Interfaces
{
	public interface ICategoryService
	{
        Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take);
        Task<GetCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateCategoryDto categoryDto);
        Task UpdateAsync(int id,UpdateCategoryDto updateCategoryDto);
        Task DeleteAsync(int id);
    }
}

