namespace Phonebook.Core.Domain.Models
{
    public class AddressVM:General
    {
        [Required]
        public string Value { get; set; }
        public long UserId { get; set; }
    }
}
