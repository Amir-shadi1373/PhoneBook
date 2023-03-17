using Phonebook.Core.Domain.Person;

namespace Phonebook.Core.Domain.Models
{
    public class ContactLists:ContactsVM
    {
        public List<PersonVM>? PersonDetailsList { get; set; }
    }
}
