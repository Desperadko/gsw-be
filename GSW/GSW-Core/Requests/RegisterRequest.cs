using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Requests
{
    public class RegisterRequest
    {
        public required string Username { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
