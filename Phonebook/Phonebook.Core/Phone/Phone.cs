namespace Phonebook.Core.Domain.Phone
{
    public class Phone:General
    {
        [Required]
        [Column(TypeName = "varchar(13)")]
        public string PhoneNumber { get; set; }

        [ForeignKey("tbl_Person")]
        public long UserId { get; set; }
        public virtual Person.Person user { get; set; }
    }
}
