using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Interfaces
{
    public interface ITestService
    {
        Task<TestModel?> GetTestRecord(int id);
        Task<bool> AddTestRecord();
    }
}
