using Phonebook.Core.Domain.Models;
using Phonebook.Framework.Extentions;

namespace Phonebook.Core.Domain.Significant_Date
{
    public interface ISignificantDateRepository
    {
        Result AddSignificantDate(SignificantDateVM significantDate);
        List<SignificantDateVM> GetSignificantDate(long UserId);
        Result RemoveUserSignificantDates(long UserId);
        Result RemoveSignificantDate(long Id);
        Result UpdateSignificantDate(long Id, SignificantDateVM significantDate);
    }
}
