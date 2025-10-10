using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.DTOs.Account
{
    public class AccountDTO
    {
        public required string Username { get; init; }
        public required string Email { get; init; }
    }
}
