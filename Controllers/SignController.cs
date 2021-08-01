using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public SignController(ApiDbContext context){
            _context = context;
        }

        [HttpGet("index")]
        public async Task<IActionResult> GetItems(){
            var items = await _context.Signs.ToListAsync();
            
            return Ok(items);
        }

        [HttpGet]
        public async Task<IActionResult> GetItem(string month, string day){      
            try {
                DateTime birthDate = DateTime.Parse($"2000-{month}-{day}");

                var item = await _context.Signs.FirstOrDefaultAsync(sign => (DateTime.Compare(birthDate, sign.MinDate) >= 0) && (DateTime.Compare(birthDate, sign.MaxDate) <= 0));

                if(item == null){
                    return NotFound();
                }

                return Ok(item);
            } catch{ 
                return new JsonResult("Something went wrong") {StatusCode = 500};
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateItem(SignData data){
            if(ModelState.IsValid){     
                await _context.Signs.AddAsync(data);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetItem", new {data.Id}, data);
            }
            
            return new JsonResult("Something went wrong") {StatusCode = 500};
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(SignData newItem){
            var existItem = await _context.Signs.FirstOrDefaultAsync(item => item.Name == newItem.Name);

            if(existItem == null){
                return NotFound();
            }

            existItem.Name = newItem.Name;
            existItem.Description = newItem.Description;
            existItem.FDetails = newItem.FDetails;
            existItem.MDetails = newItem.MDetails;
            existItem.MinDate = newItem.MinDate;
            existItem.MaxDate = newItem.MaxDate;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItem(string name){
            var existItem = await _context.Signs.FirstOrDefaultAsync(item => item.Name == name);

            if(existItem == null){
                return NotFound();
            }

            _context.Signs.Remove(existItem);
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }
    }
}