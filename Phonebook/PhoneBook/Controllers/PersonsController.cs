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
        IPersonRepository efPersonRepository;
        public PersonsController(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
            efPersonRepository = new efPersonRepository(_phonebookDbContext);
        }
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
    }
}
