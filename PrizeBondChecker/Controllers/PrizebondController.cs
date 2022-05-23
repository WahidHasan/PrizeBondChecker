using Microsoft.AspNetCore.Mvc;
using PrizeBondChecker.Domain;
using PrizeBondChecker.Domain.Prizebond;
using PrizeBondChecker.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrizeBondChecker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrizebondController : ControllerBase
    {
        private readonly PrizebondService _prizebondService;
        public PrizebondController(IPrizebondService prizebondService)
        {
            this._prizebondService = (PrizebondService?)prizebondService;
        }
        // GET: api/<PrizebondController>
        [HttpGet]
        public async Task<ActionResult<List<PrizeBond>>> Get()
        {
            return await _prizebondService.GetAllAsync();
        }

        // GET api/<PrizebondController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<PrizebondController>
        [HttpPost]
        public async Task<IActionResult> Post(PrizebondCreateModel prizeBond)
        {
            await _prizebondService.AddBondToList(prizeBond);
            return Ok(prizeBond);
        }

        //// PUT api/<PrizebondController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<PrizebondController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
