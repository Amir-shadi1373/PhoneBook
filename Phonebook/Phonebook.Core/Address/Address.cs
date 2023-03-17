namespace Phonebook.Core.Domain.Address
{
    public class Address:General
    {
        [Required]
        [Column(TypeName = "Nvarchar(500)")]
        public string Value { get; set; }

        [ForeignKey("tbl_Person")]
        public long UserId { get; set; }
        public virtual Person.Person user { get; set; }

    }
}
