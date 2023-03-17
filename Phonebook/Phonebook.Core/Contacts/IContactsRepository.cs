using Phonebook.Core.Domain.Models;
using Phonebook.Core.Domain.Person;
using Phonebook.Framework.Extentions;

namespace Phonebook.Core.Domain.Contacts
{
    public interface IContactsRepository
    {
        Result AddContact(ContactsVM inputs);
        ContactsVM GetContact(long Id);
        Result RemoveContact(long Id);
        Result UpdateContact(long Id, ContactsVM inputs);
        List<ContactResults> GetContacts();
    }
}
