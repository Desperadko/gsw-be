using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Utilities.Errors.Exceptions
{
    public class BadRequestException : FieldedException
    {
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string field, string message) : base(field, message) { }
    }
}
