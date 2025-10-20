using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Responses
{
    public class ErrorResponse
    {
        public required string Message { get; set; }
        public string?[] Details { get; set; } = [];
    }
}
