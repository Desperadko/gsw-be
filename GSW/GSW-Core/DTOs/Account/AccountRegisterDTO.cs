using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.DTOs.Account
{
    public record AccountRegisterDTO(string Username, string Email, string Password)
    {
    }
}
