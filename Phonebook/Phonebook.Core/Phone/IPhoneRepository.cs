using Phonebook.Core.Domain.Models;
using Phonebook.Framework.Extentions;

namespace Phonebook.Core.Domain.Phone
{
    public interface IPhoneRepository
    {
        Result AddPhone(PhoneVM phone);
        List<PhoneVM> GetPhone(long UserId);
        Result RemoveUserPhonees(long UserId);
        Result RemovePhone(long Id);
        Result UpdatePhone(long Id, PhoneVM phone);
    }
}
