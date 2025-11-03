using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Utilities.Errors.Exceptions
{
    public class UnauthorizedException : FieldedException
    {
        public UnauthorizedException(string message) : base(message) { }
        public UnauthorizedException(string field, string message) : base(field, message) { }
    }
}
