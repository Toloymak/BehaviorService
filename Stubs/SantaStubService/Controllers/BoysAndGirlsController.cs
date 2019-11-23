using Dao.Entities;
using Microsoft.AspNetCore.Mvc;
using Proxies.Models;

namespace SantaStubService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoysAndGirlsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string fio, int age)
        {
            var verdictDto = new VerdictDto
            {
                Fio = fio,
                Age = age,
                Verdict = VerdictType.Good
            };

            return Ok(verdictDto);
        }
    }
}