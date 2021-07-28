using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using System;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        private readonly ApiDbContext _context;

        public TodoController(ApiDbContext context){
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems(){
            var items = await _context.Items.ToListAsync();
            
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id){
            var item = await _context.Items.FirstOrDefaultAsync(item => item.Id == id);

            if(item == null){
                return NotFound();
            }

            return Ok(item);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemData data){
            if(ModelState.IsValid){
                await _context.Items.AddAsync(data);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetItem", new {data.Id}, data);
            }
            
            return new JsonResult("Something went wrong") {StatusCode = 500};
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, ItemData newItem){
            if(id != newItem.Id){
                return BadRequest();
            }

            var existItem = await _context.Items.FirstOrDefaultAsync(item => item.Id == id);

            if(existItem == null){
                return NotFound();
            }

            existItem.Title = newItem.Title;
            existItem.Description = newItem.Description;
            existItem.Done = newItem.Done;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id){
            var existItem = await _context.Items.FirstOrDefaultAsync(item => item.Id == id);

            if(existItem == null){
                return NotFound();
            }

            _context.Items.Remove(existItem);
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }
    }
}
