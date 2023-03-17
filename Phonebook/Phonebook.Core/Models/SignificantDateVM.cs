namespace Phonebook.Core.Domain.Models
{
    public class SignificantDateVM:General
    {
        [Required]
        public DateTime Date { get; set; }
        public long UserId { get; set; }
    }
}
