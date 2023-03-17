using Microsoft.AspNetCore.Mvc;
using Phonebook.Core.Domain.Models;
using Phonebook.Framework.Extentions;
using Phonebook.Infra.Data.Sql.Common;
using Phonebook.Infra.Data.Sql.Contacts;

namespace PhoneBook.Controllers
{
    [Route("Contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly PhonebookDbContext _phonebookDbContext;
        efContactsRepository _efContactRepository;
        public ContactsController(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
            _efContactRepository = new efContactsRepository(_phonebookDbContext);
        }
        [HttpPost]
        public Result Post(ContactsVM contact)
        {
            var result = _efContactRepository.AddContact(contact);
            return result;
        }
        [HttpGet]
        public ActionResult<List<ContactResults>> Get()
        {
            var result = _efContactRepository.GetContacts();
            if (result != null)
                return result;
            else
                return NotFound();
        }
        [HttpGet("{Id}")]
        public ActionResult<ContactsVM> Get(long Id)
        {
            var result = _efContactRepository.GetContact(Id);
            if (result != null)
                return result;
            else
                return NotFound();
        }
        [HttpPut("Id")]
        public Result Put(long Id, ContactsVM inputs)
        {
            var result = _efContactRepository.UpdateContact(Id, inputs);
            return result;
        }
        [HttpDelete("Id")]
        public Result Delete(long Id)
        {
            var result = _efContactRepository.RemoveContact(Id);
            return result;
        }
    }
}
