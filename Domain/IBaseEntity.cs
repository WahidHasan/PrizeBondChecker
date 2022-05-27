using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
