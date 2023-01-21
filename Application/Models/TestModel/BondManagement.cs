using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.TestModel
{
    public class BondManagement
    {
        public void AddBond(int bondId, string serial, DateTime CollectionDate)
        {
            if (!IsValidBondSerial(serial))
                throw new InvalidOperationException("Bond serial is invalid");

            if (CollectionDate.Subtract(DateTime.UtcNow).TotalDays < 30)
                throw new Exception("Collection date atleast 30 days ahead");

            Bond bond = new Bond()
            {
                BondId = bondId,
                Serial = serial,
                CollectionDate= CollectionDate
            };
        }

        private bool IsValidBondSerial(string serial)
        {
            if(string.IsNullOrWhiteSpace(serial) || serial.Length < 6)
                return false;

            return true;
        }
    }
}
