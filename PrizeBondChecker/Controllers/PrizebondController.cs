﻿using Domain.Prizebond;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("GetAllPrizebonds")]
        public async Task<ActionResult<List<Prizebond>>> GetAllPrizebond()
        {
            var response = await _prizebondService.GetAllAsync();
            return Ok(response);
        }

        // GET api/<PrizebondController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpPost("AddPrizebonds")]
        [Authorize]
        public async Task<IActionResult> AddPrizebond(PrizebondCreateModel prizeBond)
        {
            var response = await _prizebondService.AddBondToList(prizeBond);
            return Ok(response);
        }

        //// PUT api/<PrizebondController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<PrizebondController>/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(PrizebondDeleteModel prizeBond)
        {
            var response = await _prizebondService.Delete(prizeBond);
            return Ok(response);
        }
    }
}
