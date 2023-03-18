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
        /// <summary>
        /// جدول ایمیل
        /// </summary>
        public DbSet<Email> tbl_Email { get; set; }
        /// <summary>
        /// جدول اطلاعات اولیه مخاطب
        /// </summary>
        public DbSet<Core.Domain.Person.Person> tbl_Person { get; set; }
        /// <summary>
        /// جدول شماره ها
        /// </summary>
        public DbSet<Core.Domain.Phone.Phone> tbl_Phone { get; set; }
        /// <summary>
        /// جدول مناسبت های خاص
        /// </summary>
        public DbSet<SignificantDate> tbl_SignificantDate { get; set; }
        /// <summary>
        /// جدول آدرس های مخاطب
        /// </summary>
        public DbSet<Core.Domain.Address.Address> tbl_Addresses { get; set; }
        
    }
}
