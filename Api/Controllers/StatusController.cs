using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class StatusController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult Status()
        {
            var statusModel = new StatusModel("ok");
            
            return Ok(statusModel);
        }
    }
}