using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Responses.Account
{
    public class RefreshResponse
    {
        public required string Token { get; init; }
    }
}
