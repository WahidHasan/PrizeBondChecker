using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string msg) : base(msg)
        {

        }

        public virtual int ToHttpStatusCode()
        {
            return AppStatusCode.Forbidden;
        }
    }
}
