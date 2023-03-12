using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Helpers
{
    public class CommonHelper
    {
        public static string? ToTrimmedString(object value)
        {
            var stringValue = Convert.ToString(value);
            if (!string.IsNullOrWhiteSpace(stringValue))
                return stringValue?.Trim();

            return stringValue;
        }
    }
}
