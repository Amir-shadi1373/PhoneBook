using Microsoft.AspNetCore.Mvc;
using Phonebook.Core.Domain.Person;
using Phonebook.Framework.Extentions;
using Phonebook.Infra.Data.Sql.Common;
using Phonebook.Infra.Data.Sql.Person;
using System;

namespace PhoneBook.Controllers
{
    [Route("Person")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PhonebookDbContext _phonebookDbContext;
        efPersonRepository efPersonRepository;
        public PersonsController(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
            efPersonRepository = new efPersonRepository(_phonebookDbContext);
        }
        //[HttpPost]
        //public bool Post(PersonVM person)
        //{
        //    efPersonRepository.AddPerson(person);
        //    return true;
        //}
        [HttpGet]
        public ActionResult<List<PersonVM>> Get()
        {
            var result = efPersonRepository.GetAllPerson();
            if (result != null)
                return result;
            else
                return NotFound();
        }
        [HttpGet("{Id}")]
        public ActionResult<PersonVM> Get(long Id)
        {
            var result = efPersonRepository.GetPerson(Id);
            if (result != null)
                return result;
            else
                return NotFound();
        }
        //[HttpPut("Id")]
        //public Result Put(long Id, PersonVM person)
        //{
        //    var result = efPersonRepository.UpdatePerson(Id, person);
        //    return result;
        //}
        //[HttpDelete("Id")]
        //public Result Delete(long Id)
        //{
        //    var result = efPersonRepository.RemovePerson(Id);
        //    return result;
        //}
    }
}
