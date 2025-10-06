using GSW_Core.Repositories.Interfaces;
using GSW_Core.Services.Interfaces;
using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Implementations
{
    public class TestService : ITestService
    {
        private readonly ITestRepository testRepository;

        public TestService(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        public async Task<TestModel?> GetTestRecord(int id)
        {
            return await testRepository.GetTestRecord(id);
        }
        public async Task<bool> AddTestRecord()
        {
            var random = new Random();

            var result = await testRepository.AddTestRecord(random.Next(0, 10));

            if (result != 0)
                return true;

            return false;
        }
    }
}
