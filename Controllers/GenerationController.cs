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

        [HttpGet("index")]
        public async Task<IActionResult> GetItems(){
            var items = await _context.Generations.ToListAsync();
            
            return Ok(items);
        }

        [HttpGet]
        public async Task<IActionResult> GetItem(int year){
            var item = await _context.Generations.FirstOrDefaultAsync(generation => (year >= generation.MinYear) && (year <= generation.MaxYear));

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

        [HttpPut]
        public async Task<IActionResult> UpdateItem(GenerationData newItem){
            var existItem = await _context.Generations.FirstOrDefaultAsync(item => item.Name == newItem.Name);

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

        [HttpDelete]
        public async Task<IActionResult> DeleteItem(string name){
            var existItem = await _context.Generations.FirstOrDefaultAsync(item => item.Name == name);

            if(existItem == null){
                return NotFound();
            }

            _context.Generations.Remove(existItem);
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }
    }
}