using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.UploadPrizebondList
{
    public class ImportErrorViewModel
    {
        public int Row { get; set; }
        public string? Description { get; set; }
        public string? PropertyName { get; set; }
    }
}
