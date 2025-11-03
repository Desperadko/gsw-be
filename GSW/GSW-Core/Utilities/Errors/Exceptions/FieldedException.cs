using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Utilities.Errors.Exceptions
{
    public abstract class FieldedException : Exception
    {
        public string? Field { get; private set; }

        public FieldedException(string message) : base(message) { }
        public FieldedException(string field, string message) : base(message) { Field = field; }
    }
}
