using GSW_Core.Services.Interfaces;
using GSW_Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService testService;

        public TestController(ITestService testService)
        {
            this.testService = testService;
        }

        [HttpGet]
        public async Task<ActionResult<TestModel?>> GetTestRecord()
        {
            var random = new Random();

            var result = await testService.GetTestRecord(random.Next(0, 15));

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddTestRecord()
        {
            var result = await testService.AddTestRecord();
            return Ok(result);
        }
    }
}
