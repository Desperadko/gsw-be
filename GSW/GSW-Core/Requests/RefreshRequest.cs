using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Requests
{
    public class RefreshRequest
    {
        public required string Token { get; init; }
    }
}
