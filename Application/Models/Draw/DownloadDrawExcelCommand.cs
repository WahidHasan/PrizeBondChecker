using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Draw
{
    public class DownloadDrawExcelCommand
    {
        public int DrawNumber { get; set; }
        public IFormFile Image { get; set; }
    }
}
