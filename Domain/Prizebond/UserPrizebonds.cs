using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prizebond
{
    public class UserPrizebonds : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid PrizebondId { get; set; }
    }
}
