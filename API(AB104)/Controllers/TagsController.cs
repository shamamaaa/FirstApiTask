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
    public class TagsController : Controller
    {
        private readonly AppDbContext _context;
        public TagsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page, int take)
        {
            List<Tag> tags = await _context.Tags.Skip((page - 1) * take).Take(take).ToListAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Tag tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (tag is null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, tag);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Tag existed = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            bool result = _context.Tags.Any(t => t.Name.ToLower().Trim() == name.ToLower().Trim());
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

            Tag existed = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Tags.Remove(existed);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

