using Phonebook.Core.Domain.Models;
using Phonebook.Core.Domain.Person;
using Phonebook.Framework.Extentions;

namespace Phonebook.Core.Domain.Address
{
    public interface IAddressRepository
    {
        Result AddAddress(AddressVM address);
        List<AddressVM> GetAddress(long UserId);
        Result RemoveUserAddresses(long UserId);
        Result RemoveAddress(long Id);
        Result UpdateAddress(long Id, AddressVM address);
    }
}
