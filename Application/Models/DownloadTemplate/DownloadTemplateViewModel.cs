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
        public string Serial { get; set; } = string.Empty;

        [DisplayName("Bond Id In Bengali")]
        public string BondIdInBengali { get; set; } = string.Empty;

        [DisplayName("Notes")]
        public Guid Notes { get; set; }
    }
}
