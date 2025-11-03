using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Utilities.Errors.Exceptions
{
    public class NotFoundException : FieldedException
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string field, string message) : base(field, message) { }
    }
}
