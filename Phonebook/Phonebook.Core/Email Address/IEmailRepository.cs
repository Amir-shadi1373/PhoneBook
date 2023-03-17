using Phonebook.Core.Domain.Models;
using Phonebook.Framework.Extentions;

namespace Phonebook.Core.Domain.Email_Address
{
    public interface IEmailRepository
    {
        Result AddEmail(EmailVM email);
        List<EmailVM> GetEmail(long UserId);
        Result RemoveUserEmailes(long UserId);
        Result RemoveEmail(long Id);
        Result UpdateEmail(long Id, EmailVM email);
    }
}
