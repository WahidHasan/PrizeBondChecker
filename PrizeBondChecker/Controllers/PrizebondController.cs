﻿using Application.Models.CheckBond;
using Application.Models.Draw;
using Application.Models.PrizebondView;
using Application.Models.UploadPrizebondList;
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

        [HttpGet("DownloadPrizebondUploadTemplate")]
        [Authorize]
        public async Task<IActionResult> DownloadPrizebondTemplate()
        {
            var stream = await _prizebondService.DownloadPrizebondTemplate();
            string templateName = "PrizebondUploadTemplate";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", templateName);
        }

        [HttpPost("UploadPrizebondList")]
        [Authorize]
        public async Task<IActionResult> UploadPrizebondList([FromForm] UploadPrizebondListCommand command)
        {
            var response = await _prizebondService.UploadPrizebondList(command);
            return Ok(response);
        }

        [HttpPost("DownloadDrawExcelFile")]
        [Authorize]
        public async Task<IActionResult> DownloadDrawExcelFile([FromForm] DownloadDrawExcelCommand command)
        {
            var stream = await _prizebondService.DownloadDrawExcelFile(command);
            string templateName = $"{command.DrawNumber}_DrawUploadTemplate";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", templateName);
        }

        [HttpPost("AddNewDraw")]
        [Authorize]
        public async Task<IActionResult> AddNewDraw([FromForm] AddNewDrawCommand prizeBond)
        {
            var response = await _prizebondService.AddNewDraw(prizeBond);
            return Ok(response);
        }

        [HttpPost("CheckUserBondsWithDraw")]
        [Authorize]
        public async Task<IActionResult> CheckUserBondsWithDraw(CheckUserBondsListCommand command)
        {
            var response = await _prizebondService.CheckUserBondsWithDraw(command);
            return Ok(response);
        }

        [HttpPost("CheckWithAllClaimableDraw")]
        [Authorize]
        public async Task<IActionResult> CheckWithAllClaimableDraw([FromForm] AddNewDrawCommand prizeBond)
        {
            var response = await _prizebondService.AddNewDraw(prizeBond);
            return Ok(response);
        }
    }
}
