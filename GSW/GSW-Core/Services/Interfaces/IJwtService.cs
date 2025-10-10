using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Account account);
    }
}
