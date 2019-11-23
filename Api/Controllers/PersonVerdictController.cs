using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Proxies;
using Proxies.Configs;
using RestSharp;

namespace Api.Controllers
{
    public class PersonVerdictController : ApiControllerBase
    {
        private readonly SantaAppProxy santaAppProxy;
        private readonly PerNoelAppProxy perNoelAppProxy;
        
        public PersonVerdictController(SantaAppProxy santaAppProxy,
                                       PerNoelAppProxy perNoelAppProxy)
        {
            this.santaAppProxy = santaAppProxy;
            this.perNoelAppProxy = perNoelAppProxy;
        }
        
        [HttpGet]
        public IActionResult Get(string fio, [Required] int age)
        {
            var verdictDto = santaAppProxy.GetVerdict(fio, age);

            return Ok(verdictDto);
        }
    }
}