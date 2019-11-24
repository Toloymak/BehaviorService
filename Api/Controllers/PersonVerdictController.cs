using System.ComponentModel.DataAnnotations;
using System.Net;
using Dao.Core.Persons;
using Dao.Entities;
using Microsoft.AspNetCore.Mvc;
using Proxies;
using Proxies.Models;

namespace Api.Controllers
{
    public class PersonVerdictController : ApiControllerBase
    {
        private readonly SantaAppProxy santaAppProxy;
        private readonly PerNoelAppProxy perNoelAppProxy;
        private readonly PersonReader personReader;
        private readonly PersonWriter personWriter;
        public PersonVerdictController(SantaAppProxy santaAppProxy,
                                       PerNoelAppProxy perNoelAppProxy,
                                       PersonReader personReader,
                                       PersonWriter personWriter)
        {
            this.santaAppProxy = santaAppProxy;
            this.perNoelAppProxy = perNoelAppProxy;
            this.personReader = personReader;
            this.personWriter = personWriter;
        }
        
        [HttpGet]
        public ActionResult<Person> Get(string fio, [Required] int age)
        {
            var person = personReader.GetByFioAndAge(fio, age);
            if (person == null)
            {
                VerdictDto verdictDto;

                if (TryGetVerdict(santaAppProxy, fio, age, out verdictDto) 
                    || TryGetVerdict(perNoelAppProxy, fio, age, out verdictDto))
                {
                    person = new Person()
                    {
                        Fio = verdictDto.Fio,
                        Age = verdictDto.Age,
                        Verdict = verdictDto.Verdict
                    };

                    personWriter.Create(person);
                }
            }

            if (person == null)
                return NotFound($"Информация о поведении {fio} не найдена");

            return Ok(person);
        }
        
        [HttpGet]
        [Route("fromSanta")]
        public ActionResult<Person> GetFromSanta(string fio, [Required] int age)
        {
            return GetByConcreteProxy(santaAppProxy, fio, age);
        }
        
        [HttpGet]
        [Route("fromperNoel")]
        public ActionResult<Person> GetFromPerNoel(string fio, [Required] int age)
        {
            return GetByConcreteProxy(perNoelAppProxy, fio, age);
        }
        
        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {
            var persons = personReader.All;
            
            return Ok(persons);
        }

        private bool TryGetVerdict(IBehaviorProxy behaviorProxy, string fio, int age, out VerdictDto verdictDto)
        {
            var verdictResult = behaviorProxy.GetVerdict(fio, age);

            if (verdictResult.StatusCode != HttpStatusCode.OK)
            {
                verdictDto = null;
                return false;
            }

            verdictDto = verdictResult.VerdictDto;
            return true;
        }

        private ActionResult<Person> GetByConcreteProxy(IBehaviorProxy behaviorProxy, string fio, int age)
        {
            var verdictResult = behaviorProxy.GetVerdict(fio, age);
            if (verdictResult.StatusCode != HttpStatusCode.OK)
                return NotFound($"Данные с сервиса санты получить не удалось. Ошибка: {verdictResult.ErrorMessage}");

            var personFromDb = personReader.GetByFioAndAge(fio, age);
            if (personFromDb != null)
                return Ok(personFromDb);
            
            var person = new Person()
            {
                Fio = verdictResult.VerdictDto.Fio,
                Age = verdictResult.VerdictDto.Age,
                Verdict = verdictResult.VerdictDto.Verdict
            };

            personWriter.Create(person);

            return Ok(person);
        }
    }
}