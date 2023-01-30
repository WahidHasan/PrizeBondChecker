using Domain.Prizebond;
using Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.TestModel
{
    public class BondManagement
    {
        private readonly IRepository<UserPrizebonds> _userPrizebonds;
        public BondManagement(IRepository<UserPrizebonds> userPrizebonds)
        {
            _userPrizebonds = userPrizebonds;
        }
        public void AddBond(int bondId, string serial, DateTime CollectionDate)
        {
            if (!IsValidBondSerial(serial))
                throw new InvalidOperationException("Bond serial is invalid");

            if (DateTime.UtcNow.Subtract(CollectionDate).TotalDays < 30)
                throw new Exception("Collection date atleast 30 days ahead");

            Bond bond = new Bond()
            {
                BondId = bondId,
                Serial = serial,
                CollectionDate= CollectionDate
            };
        }

        public void AddActualBond(string bondId, string serial, Guid userId)
        {
            if (!IsValidBondSerial(serial))
                throw new InvalidOperationException("Bond serial is invalid");

            var bond = new UserPrizebonds()
            {
                BondId = bondId,
                Serial = serial,
                UserId = userId
            };

            _userPrizebonds.InsertOne(bond);
        }

        private bool IsValidBondSerial(string serial)
        {
            if(string.IsNullOrWhiteSpace(serial) || !(serial.Length > 1 && serial.Length < 6))
                return false;

            return true;
        }
    }
}
