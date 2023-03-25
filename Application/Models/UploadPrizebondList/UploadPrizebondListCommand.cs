using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.UploadPrizebondList
{
    public class UploadPrizebondListCommand
    {
        public Guid UserId { get; set; }
        public IFormFile File { get; set; }
    }
}
