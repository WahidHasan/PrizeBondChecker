using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DBContext
{
    public class DbConnectionDetails 
    {
        public string PrizebondCheckerCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
