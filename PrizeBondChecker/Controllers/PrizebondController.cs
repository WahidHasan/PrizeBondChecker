using Application.Models.Draw;
using Application.Models.PrizebondView;
using Domain.Prizebond;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrizeBondChecker.Domain.Prizebond;
using PrizeBondChecker.Services;
using SharpCompress.Compressors.Xz;
using System.IO;

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
            _prizebondService = (PrizebondService?)prizebondService;
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

        [HttpGet("DownloadTemplate")]
        [Authorize]
        public async Task<IActionResult> DownloadPrizebondTemplate()
        {
            var stream = await _prizebondService.DownloadPrizebondTemplate();
            string templateName = "PrizebondUploadTemplate";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", templateName);
        }

        [HttpPost("DownloadDrawExcelFile")]
        [Authorize]
        public async Task<IActionResult> DownloadDrawExcelFile([FromForm] DownloadDrawExcelCommand command)
        {
            var stream = await _prizebondService.DownloadDrawExcelFile(command);
            string templateName = "DrawUploadTemplate";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", templateName);
        }

        [HttpPost("AddNewDraw")]
        [Authorize]
        public async Task<IActionResult> AddNewDraw([FromForm] AddNewDrawCommand prizeBond)
        {
            var response = await _prizebondService.AddNewDraw(prizeBond);
            return Ok(response);
        }
    }
}
