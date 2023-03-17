using Microsoft.EntityFrameworkCore;
using Phonebook.Core.Domain.EmailAddress;
using Phonebook.Core.Domain.SignificantDate;

namespace Phonebook.Infra.Data.Sql.Common
{
    public class PhonebookDbContext : DbContext
    {
        public PhonebookDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Email> tbl_Email { get; set; }
        public DbSet<Core.Domain.Person.Person> tbl_Person { get; set; }
        public DbSet<Core.Domain.Phone.Phone> tbl_Phone { get; set; }
        public DbSet<SignificantDate> tbl_SignificantDate { get; set; }
        public DbSet<Core.Domain.Address.Address> tbl_Addresses { get; set; }
        
    }
}
