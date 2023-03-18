using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CheckBond
{
    public class CheckUserBondsListCommand
    {
        public int DrawNumber { get; set; }
        public Guid UserId { get; set; }
    }
}
