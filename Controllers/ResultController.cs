using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ResultController(ApiDbContext context){
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetItem(string day, string month, int year, string name){      
            try {
                // sign
                DateTime birthDate = DateTime.Parse($"2000-{month}-{day}");

                var sign = await _context.Signs.FirstOrDefaultAsync(sign => (DateTime.Compare(birthDate, sign.MinDate) >= 0) && (DateTime.Compare(birthDate, sign.MaxDate) <= 0));

                if(sign == null){
                    throw new ArgumentNullException();
                }

                // generation
                var generation = await _context.Generations.FirstOrDefaultAsync(generation => (year >= generation.MinYear) && (year <= generation.MaxYear));

                if(generation == null){
                    throw new ArgumentNullException();
                }

                // names
                var apiKey = "ga090080281";
                var httpClient = new HttpClient();

                var lookupNameUrl = $"https://www.behindthename.com/api/lookup.json?name={name}&key={apiKey}";
                var lookupNameResponse = await httpClient.GetAsync(lookupNameUrl);
                string lookupName = await lookupNameResponse.Content.ReadAsStringAsync();
                
                if(lookupName.Contains("error")){
                    throw new ArgumentNullException();
                }

                var relatedNameUrl = $"https://www.behindthename.com/api/related.json?name={name}&key={apiKey}";
                var relatedNameResponse = await httpClient.GetAsync(relatedNameUrl);
                string relatedName = await relatedNameResponse.Content.ReadAsStringAsync();

                if(relatedName.Contains("error")){
                    throw new ArgumentNullException();
                }

                // return new JsonResult(json);
                return new JsonResult(new { sign, generation, lookupName, relatedName});
            } catch{ 
                return new JsonResult("Something went wrong") {StatusCode = 500};
            }
        }
    }
}