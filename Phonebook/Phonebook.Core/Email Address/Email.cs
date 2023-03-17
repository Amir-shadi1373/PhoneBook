namespace Phonebook.Core.Domain.EmailAddress
{
    public class Email:General
    {
        [Required]
        [StringLength(200)]
        public string Value { get; set; }

        [ForeignKey("tbl_Person")]
        public long UserId { get; set; }
        public virtual Person.Person user { get; set; }
    }
}
