using PrizeBondChecker.Domain.Enums;
using PrizeBondChecker.Domain.Prizebond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeBondChecker.Test.MockData
{
    public class PrizebondMockdata
    {
        public static List<Prizebond> GetAll()
        {
            return new List<Prizebond>()
            {
                new Prizebond
                {
                    UserId= new Guid("2e5d8dcd-3e09-453c-90b5-348a0801cc0f"),
                    serial= "HA",
                    bondId= "010101",
                    entryDate= DateTime.Parse("2022-06-08T17:51:31.61Z"),
                    Checked= EnumPrizebond.CheckBond.Checked
                },
                new Prizebond
                {
                    UserId= new Guid("2e5d8dcd-3e09-453c-90b5-348a0801cc0f"),
                    serial= "HP",
                    bondId= "010102",
                    entryDate= DateTime.Parse("2022-06-08T17:51:31.61Z"),
                    Checked= EnumPrizebond.CheckBond.Checked
                },
                new Prizebond
                {
                    UserId= new Guid("2e5d8dcd-3e09-453c-90b5-348a0801cc0f"),
                    serial= "HS",
                    bondId= "010103",
                    entryDate= DateTime.Parse("2022-06-08T17:51:31.61Z"),
                    Checked= EnumPrizebond.CheckBond.Checked
                }
            };
        }
    }
}
