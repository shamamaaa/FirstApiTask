using System.Xml.Linq;
using API_AB104_.Repostories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AB104_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repository;

        public CategoriesController(AppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 2)
        {
            IEnumerable<Category> categories = await _repository.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto categoryDto)
        {
            bool result = _context.Categories.Any(t => t.Name.ToLower().Trim() == categoryDto.Name.ToLower().Trim());
            if (result)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            Category category = new()
            {
                Name = categoryDto.Name,
            };


            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] Category category)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category existed = await _repository.GetByIdAsync(id);
            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            bool result = _context.Categories.Any(t => t.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if (result)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            existed.Name = category.Name;

            _repository.Update(category);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) return StatusCode(StatusCodes.Status404NotFound);
            _repository.Delete(category);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}

