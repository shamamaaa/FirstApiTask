using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_AB104_.DAL;
using API_AB104_.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AB104_.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page, int take)
        {
            List<Category> categories = await _context.Categories.Skip((page-1)*take).Take(take).ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK,category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            bool result = _context.Categories.Any(t => t.Name.ToLower().Trim() == name.ToLower().Trim());
            if (result)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            existed.Name = name;
            await _context.SaveChangesAsync();
            return NoContent();


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Categories.Remove(existed);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

