using Microsoft.EntityFrameworkCore;
using Phonebook.Core.Domain.Models;
using Phonebook.Core.Domain.Significant_Date;
using Phonebook.Framework.Extentions;
using Phonebook.Infra.Data.Sql.Common;

namespace Phonebook.Infra.Data.Sql.Significant_Date
{
    public class efSignificantDateRepository: ISignificantDateRepository
    {
        private readonly PhonebookDbContext _phonebookDbContext;
        Core.Domain.SignificantDate.SignificantDate? _SignificantDate;
        Result result = new Result();
        SignificantDateVM? _significantDateVM;
        List<SignificantDateVM>? li;
        public efSignificantDateRepository(PhonebookDbContext significantDatebookDbContext)
        {
            _phonebookDbContext = significantDatebookDbContext;
        }
        public Result AddSignificantDate(SignificantDateVM significantDate)
        {
            try
            {
                _SignificantDate = new Core.Domain.SignificantDate.SignificantDate();
                _SignificantDate.Date = significantDate.Date;
                _SignificantDate.UserId = significantDate.UserId;

                _phonebookDbContext.tbl_SignificantDate.Add(_SignificantDate);
                _phonebookDbContext.SaveChanges();

                result.ResultStatus = true;
                result.ResultMessage = "Success!";
                return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = false;
                result.ResultMessage = ex.Message;
                return result;
            }
        }

        public List<SignificantDateVM> GetSignificantDate(long UserId)
        {
            try
            {
                List<Core.Domain.SignificantDate.SignificantDate> significantDates = _phonebookDbContext.tbl_SignificantDate.Where(a => a.UserId == UserId).ToList();
                if (significantDates != null)
                {
                    li = new List<SignificantDateVM>();
                    foreach (var item in significantDates)
                    {
                        _significantDateVM = new SignificantDateVM();
                        _significantDateVM.Date = item.Date;
                        _significantDateVM.UserId = item.UserId;
                        _significantDateVM.Id = item.Id;
                        li.Add(_significantDateVM);
                    }
                    return li;
                }
                _significantDateVM = new SignificantDateVM();
                return li;
            }
            catch
            {
                _significantDateVM = new SignificantDateVM();
                return li;
            }
        }
        public List<SignificantDateVM> GetSignificantDates()
        {
            try
            {
                List<Core.Domain.SignificantDate.SignificantDate> significantDates = _phonebookDbContext.tbl_SignificantDate.ToList();
                if (significantDates != null)
                {
                    li = new List<SignificantDateVM>();
                    foreach (var item in significantDates)
                    {
                        _significantDateVM = new SignificantDateVM();
                        _significantDateVM.Date = item.Date;
                        _significantDateVM.UserId = item.UserId;
                        _significantDateVM.Id = item.Id;
                        li.Add(_significantDateVM);
                    }
                    return li;
                }
                _significantDateVM = new SignificantDateVM();
                return li;
            }
            catch
            {
                _significantDateVM = new SignificantDateVM();
                return li;
            }
        }

        public Result RemoveSignificantDate(long Id)
        {
            try
            {
                var significantDates = _phonebookDbContext.tbl_SignificantDate.Where(a => a.Id == Id).SingleOrDefault();
                if (significantDates != null)
                {
                    _phonebookDbContext.tbl_SignificantDate.Remove(significantDates);
                    _phonebookDbContext.SaveChanges();
                    result.ResultStatus = true;
                    result.ResultMessage = "success!";
                    return result;
                }
                result.ResultStatus = false;
                result.ResultMessage = "significantDate Is Null!";
                return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = false;
                result.ResultMessage = ex.Message;
                return result;
            }
        }

        public Result RemoveUserSignificantDates(long UserId)
        {
            try
            {
                List<Core.Domain.SignificantDate.SignificantDate>? significantDates = _phonebookDbContext.tbl_SignificantDate.Where(a => a.UserId == UserId).ToList();
                if (significantDates != null)
                {
                    _phonebookDbContext.tbl_SignificantDate.RemoveRange(significantDates);
                    _phonebookDbContext.SaveChanges();
                    result.ResultStatus = true;
                    result.ResultMessage = "success!";
                    return result;
                }
                result.ResultStatus = false;
                result.ResultMessage = "significantDate Is Null!";
                return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = false;
                result.ResultMessage = ex.Message;
                return result;
            }
        }

        public Result UpdateSignificantDate(long Id, SignificantDateVM significantDate)
        {
            try
            {
                Core.Domain.SignificantDate.SignificantDate? significantDates = _phonebookDbContext.tbl_SignificantDate.Where(a => a.Id == Id).SingleOrDefault();
                if (significantDates != null)
                {
                    if (significantDate.Date!=null)
                        significantDates.Date = significantDate.Date;
                    _phonebookDbContext.Entry(significantDates).State = EntityState.Modified;
                    _phonebookDbContext.SaveChanges();
                    result.ResultStatus = true;
                    result.ResultMessage = "Success!";
                    return result;
                }
                else
                {
                    result.ResultStatus = false;
                    result.ResultMessage = "Significant Date Number Is Null!";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = false;
                result.ResultMessage = ex.Message;
                return result;
            }
        }
    }
}
