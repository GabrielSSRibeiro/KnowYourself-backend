using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerationController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public GenerationController(ApiDbContext context){
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems(){
            var items = await _context.Generations.ToListAsync();
            
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id){
            var item = await _context.Generations.FirstOrDefaultAsync(item => item.Id == id);

            if(item == null){
                return NotFound();
            }

            return Ok(item);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateItem(GenerationData data){
            if(ModelState.IsValid){
                await _context.Generations.AddAsync(data);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetItem", new {data.Id}, data);
            }
            
            return new JsonResult("Something went wrong") {StatusCode = 500};
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, GenerationData newItem){
            if(id != newItem.Id){
                return BadRequest();
            }

            var existItem = await _context.Generations.FirstOrDefaultAsync(item => item.Id == id);

            if(existItem == null){
                return NotFound();
            }

            existItem.Name = newItem.Name;
            existItem.Description = newItem.Description;
            existItem.MinYear = newItem.MinYear;
            existItem.MaxYear = newItem.MaxYear;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id){
            var existItem = await _context.Generations.FirstOrDefaultAsync(item => item.Id == id);

            if(existItem == null){
                return NotFound();
            }

            _context.Generations.Remove(existItem);
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }
    }
}