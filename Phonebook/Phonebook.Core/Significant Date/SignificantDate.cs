namespace Phonebook.Core.Domain.SignificantDate
{
    public class SignificantDate:General
    {
        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("tbl_Person")]
        public long UserId { get; set; }
        public virtual Person.Person user { get; set; }
    }
}
