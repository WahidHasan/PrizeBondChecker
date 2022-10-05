using Application.Models.PrizebondView;
using Domain.Prizebond;
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

        //[HttpGet("GetAllPrizebonds")]
        //public async Task<ActionResult<List<Prizebond>>> GetAllPrizebond()
        //{
        //    var response = await _prizebondService.GetAllAsync();
        //    return Ok(response);
        //}

        [HttpPost("AddPrizebonds")]
        [Authorize]
        public async Task<IActionResult> AddPrizebond(PrizebondCreateModel prizeBond)
        {
            var response = await _prizebondService.AddBondToList(prizeBond);
            return Ok(response);
        }

        [HttpPost("GetByUserId")]
        [Authorize]
        public async Task<IActionResult> GetByUserId(PrizebondRequestQuery query)
        {
            var response = await _prizebondService.GetByUserIdAsync(query);
            return Ok(response);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(PrizebondDeleteModel prizeBond)
        {
            var response = await _prizebondService.Delete(prizeBond);
            return Ok(response);
        }
    }
}
