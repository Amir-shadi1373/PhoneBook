namespace Phonebook.Core.Domain.Person
{
    public class PersonVM:General
    {
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NikName { get; set; }
        public string? Company { get; set; }
        public string? Notes { get; set; }
        public string? UserPicture { get; set; }
    }
}
