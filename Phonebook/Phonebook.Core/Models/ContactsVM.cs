using Phonebook.Core.Domain.Person;

namespace Phonebook.Core.Domain.Models
{
    public class ContactsVM
    {
        [Required]
        public PersonVM PersonDetails { get; set; }
        [Required]
        public List<PhoneVM> PhoneList { get; set; }
        public List<AddressVM>? AddressList { get; set; }
        public List<EmailVM>? EmailList { get; set; }
        public List<SignificantDateVM>? SignificantDateList { get; set; }

    }
}
