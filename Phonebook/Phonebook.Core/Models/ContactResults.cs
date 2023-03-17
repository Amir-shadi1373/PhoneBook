using Phonebook.Core.Domain.Person;

namespace Phonebook.Core.Domain.Models
{
    public class ContactResults
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Notes { get; set; }
        public string? Company { get; set; }
        public string? NikName { get; set; }
        public List<EmailVM>? EmailValueList { get; set; }
        public List<PhoneVM>? PhoneNumberList { get; set; }
        public List<AddressVM>? addressList { get; set; }
        public List<SignificantDateVM>? significantDateList { get; set; }
    }
}
