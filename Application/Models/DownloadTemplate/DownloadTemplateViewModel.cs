using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DownloadTemplate
{
    public class DownloadTemplateViewModel
    {
        [DisplayName("Serial No")]
        public string Serial { get; set; }

        [DisplayName("Bond Id")]
        public Guid BondId { get; set; }
    }
}
