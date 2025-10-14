using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Responses
{
    public class ErrorResponse
    {
        public required string Error { get; set; }
        public string?[] Details { get; set; } = [];
    }
}
