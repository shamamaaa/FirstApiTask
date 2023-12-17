using System;
namespace API_AB104_.Services.Interfaces
{
	public interface ITagService
	{
        Task<ICollection<GetTagDto>> GetAllAsync(int page, int take);
        Task<GetTagDto> GetByIdAsync(int id);
        Task CreateAsync(CreateTagDto tagDto);
        Task UpdateAsync(int id,UpdateTagDto updateTagDto);
        Task DeleteAsync(int id);
    }
}

