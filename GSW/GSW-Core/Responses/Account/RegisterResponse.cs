using GSW_Core.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Responses.Account
{
    public class RegisterResponse
    {
        public required string Token { get; init; }
        public required AccountDTO Account { get; init; }
    }
}
