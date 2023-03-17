namespace Phonebook.Core.Domain.Models
{
    public class PhoneVM:General
    {
        [Required]
        public string PhoneNumber { get; set; }
        public long UserId { get; set; }
    }
}
