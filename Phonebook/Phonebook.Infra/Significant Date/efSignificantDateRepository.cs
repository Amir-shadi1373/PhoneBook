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
        public efSignificantDateRepository(PhonebookDbContext significantDatebookDbContext)
        {
            _phonebookDbContext = significantDatebookDbContext;
        }
        /// <summary>
        /// افزودن تاریح مناسبت خاص
        /// </summary>
        /// <param name="significantDate">تاریخ روز خاص و آیدی مخاطب</param>
        /// <returns></returns>
        public Result AddSignificantDate(SignificantDateVM significantDate)
        {
            try
            {
                Core.Domain.SignificantDate.SignificantDate _SignificantDate = new Core.Domain.SignificantDate.SignificantDate();
                Result result = new Result();
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
                Result result = new Result();
                result.ResultStatus = false;
                result.ResultMessage = ex.Message;
                return result;
            }
        }
        /// <summary>
        /// لیستی از روز های خاص مخاطب مورد نظر
        /// </summary>
        /// <param name="UserId">آیدی مخاطب</param>
        /// <returns></returns>
        public List<SignificantDateVM> GetSignificantDate(long UserId)
        {
            List<SignificantDateVM> li = new List<SignificantDateVM>();
            try
            {
                List<Core.Domain.SignificantDate.SignificantDate> significantDates = _phonebookDbContext.tbl_SignificantDate.Where(a => a.UserId == UserId).ToList();
                    
                if (significantDates != null)
                {
                    foreach (var item in significantDates)
                    {
                        SignificantDateVM _significantDateVM = new SignificantDateVM();
                        _significantDateVM.Date = item.Date;
                        _significantDateVM.UserId = item.UserId;
                        _significantDateVM.Id = item.Id;
                        li.Add(_significantDateVM);
                    }
                    return li;
                }
                return li;
            }
            catch
            {
                return li;
            }
        }
        /// <summary>
        /// لیست روز های خاص تمام مخاطبین
        /// </summary>
        /// <returns></returns>
        public List<SignificantDateVM> GetSignificantDates()
        {
                    List<SignificantDateVM> li = new List<SignificantDateVM>();
            try
            {
                List<Core.Domain.SignificantDate.SignificantDate> significantDates = _phonebookDbContext.tbl_SignificantDate.ToList();
                if (significantDates != null)
                {
                    foreach (var item in significantDates)
                    {
                        SignificantDateVM _significantDateVM = new SignificantDateVM();
                        _significantDateVM.Date = item.Date;
                        _significantDateVM.UserId = item.UserId;
                        _significantDateVM.Id = item.Id;
                        li.Add(_significantDateVM);
                    }
                    return li;
                }
                return li;
            }
            catch
            {
                return li;
            }
        }
        /// <summary>
        /// متد حذف یک تاریخ خاص
        /// </summary>
        /// <param name="Id">آیدی مخاطب</param>
        /// <returns></returns>
        public Result RemoveSignificantDate(long Id)
        {
                Result result = new Result();
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
        /// <summary>
        /// متد حذف همه روز های خاص تمام مخاطبین
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Result RemoveUserSignificantDates(long UserId)
        {
            Result result = new Result();
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
        /// <summary>
        /// بروز رسانی روزهای خاص یک مخاطب
        /// </summary>
        /// <param name="Id">آیدی مخاطب</param>
        /// <param name="significantDate">تاریخ روز خاص و آیدی مخاطب</param>
        /// <returns></returns>
        public Result UpdateSignificantDate(long Id, SignificantDateVM significantDate)
        {
            Result result = new Result();
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
