using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        [HttpGet("lookup")]
        public async Task<IActionResult> LookupItems(string name){

            var apiKey = "ga090080281";
            var httpClient = new HttpClient();

            var url = $"https://www.behindthename.com/api/lookup.json?name={name}&key={apiKey}";
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            
            if(result.Contains("error")){
                return NotFound();
            }

            return Ok(result);        
        }

        [HttpGet("related")]
        public async Task<IActionResult> RelatedItems(string name){

            var apiKey = "ga090080281";
            var httpClient = new HttpClient();

            var url = $"https://www.behindthename.com/api/related.json?name={name}&key={apiKey}";
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            if(result.Contains("error")){
                return NotFound();
            }

            return Ok(result);           
        }
    }
}