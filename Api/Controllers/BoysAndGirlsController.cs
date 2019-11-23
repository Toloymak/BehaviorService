using System.ComponentModel.DataAnnotations;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class BoysAndGirlsController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult Get(string fio, [Required] int age)
        {
            var verdictDto = new VerdictDto();

            return Ok(verdictDto);
        }
    }
}