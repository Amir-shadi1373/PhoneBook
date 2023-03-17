namespace Phonebook.Core.Domain.Person
{
    public class Person:General
    {
        [Required]
        [Column(TypeName = "Nvarchar(200)")]
        public string FirstName { get; set; }
        [Column(TypeName = "Nvarchar(200)")]
        public string? LastName { get; set; }
        [Column(TypeName = "Nvarchar(200)")]
        public string? NikName { get; set; }
        [Column(TypeName = "Nvarchar(100)")]
        public string? Company { get; set; }
        [Column(TypeName = "Nvarchar(200)")]
        public string? Notes { get; set; }
        [Column(TypeName = "Nvarchar(200)")]
        public string? UserPicture { get; set; }

        public virtual List<Address.Address> vAddress { get; set; }
        public virtual List<Phone.Phone> vPhone { get; set; }
        public virtual List<SignificantDate.SignificantDate> vSignificantDate { get; set; }
        public virtual List<EmailAddress.Email> vEmail { get; set; }
    }
}
