using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Responses
{
    public abstract class BaseResponse
    {
        public string Error { get; set; } = string.Empty;
    }
}
