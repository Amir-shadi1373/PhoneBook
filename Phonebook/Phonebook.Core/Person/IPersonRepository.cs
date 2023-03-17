using Phonebook.Framework.Extentions;

namespace Phonebook.Core.Domain.Person
{
    public interface IPersonRepository
    {
        long AddPerson(PersonVM person);
        PersonVM GetPerson(long Id);
        Result RemovePerson(long Id);
        Result UpdatePerson(long Id, PersonVM person);
        List<PersonVM> GetAllPerson();
    }
}
