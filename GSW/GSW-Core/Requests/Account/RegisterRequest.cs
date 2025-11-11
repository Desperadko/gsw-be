using GSW_Core.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Requests.Account
{
    public record RegisterRequest(AccountRegisterDTO Credentials)
    {
    }
}
