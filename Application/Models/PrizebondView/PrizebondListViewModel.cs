using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.PrizebondView
{
    public class PrizebondListViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Serial { get; set; } = string.Empty;
        public string BondId { get; set; } = string.Empty;
        public string? BondIdInBengali { get; set; }
    }
}
