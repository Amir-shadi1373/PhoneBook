using Microsoft.EntityFrameworkCore;
using Phonebook.Core.Domain.Models;
using Phonebook.Core.Domain.Phone;
using Phonebook.Framework.Extentions;
using Phonebook.Infra.Data.Sql.Common;

namespace Phonebook.Infra.Data.Sql.Phone
{
    public class efPhoneRepository : IPhoneRepository
    {
        private readonly PhonebookDbContext _phonebookDbContext;
        public efPhoneRepository(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
        }
        /// <summary>
        /// افزودن یک شماره 
        /// </summary>
        /// <param name="phone">شماره تلفن/موبایل</param>
        /// <returns></returns>
        public Result AddPhone(PhoneVM phone)
        {
                Core.Domain.Phone.Phone _Phone = new Core.Domain.Phone.Phone();
            Result result = new Result();
            try
            {
                _Phone.PhoneNumber = phone.PhoneNumber;
                _Phone.UserId = phone.UserId;

                _phonebookDbContext.tbl_Phone.Add(_Phone);
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
        /// <summary>
        /// فراخوانی شماره های مربوط به یک مخاطب
        /// </summary>
        /// <param name="UserId">آیدی مخاطب مورد نظر</param>
        /// <returns></returns>
        public List<PhoneVM> GetPhone(long UserId)
        {
                   List<PhoneVM> li = new List<PhoneVM>();
            try
            {
                List<Core.Domain.Phone.Phone> phones = _phonebookDbContext.tbl_Phone.Where(a => a.UserId == UserId).ToList();
                if (phones != null)
                {
                    foreach (var item in phones)
                    {
                        PhoneVM _phoneVM = new PhoneVM();
                        _phoneVM.PhoneNumber = item.PhoneNumber;
                        _phoneVM.UserId = item.UserId;
                        _phoneVM.Id = item.Id;
                        li.Add(_phoneVM);
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
        /// فراخوانی همه شماره های موجود
        /// </summary>
        /// <returns></returns>
        public List<PhoneVM> GetPhones()
        {
                   List<PhoneVM> li = new List<PhoneVM>();
            try
            {
                List<Core.Domain.Phone.Phone> phones = _phonebookDbContext.tbl_Phone.ToList();
                if (phones != null)
                {
                    foreach (var item in phones)
                    {
                        PhoneVM _phoneVM = new PhoneVM();
                        _phoneVM.PhoneNumber = item.PhoneNumber;
                        _phoneVM.UserId = item.UserId;
                        _phoneVM.Id = item.Id;
                        li.Add(_phoneVM);
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
        /// حذف یک شماره
        /// </summary>
        /// <param name="Id">آیدی</param>
        /// <returns></returns>
        public Result RemovePhone(long Id)
        {
            Result result = new Result();
            try
            {
                var phones = _phonebookDbContext.tbl_Phone.Where(a => a.Id == Id).SingleOrDefault();
                if (phones != null)
                {
                    _phonebookDbContext.tbl_Phone.Remove(phones);
                    _phonebookDbContext.SaveChanges();
                    result.ResultStatus = true;
                    result.ResultMessage = "success!";
                    return result;
                }
                result.ResultStatus = false;
                result.ResultMessage = "Address Is Null!";
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
        /// حذف تمامی شماره های یک مخاطب
        /// </summary>
        /// <param name="UserId">آیدی مخاطب</param>
        /// <returns></returns>
        public Result RemoveUserPhonees(long UserId)
        {
            Result result = new Result();
            try
            {
                List<Core.Domain.Phone.Phone>? phones = _phonebookDbContext.tbl_Phone.Where(a => a.UserId == UserId).ToList();
                if (phones != null)
                {
                    _phonebookDbContext.tbl_Phone.RemoveRange(phones);
                    _phonebookDbContext.SaveChanges();
                    result.ResultStatus = true;
                    result.ResultMessage = "success!";
                    return result;
                }
                result.ResultStatus = false;
                result.ResultMessage = "Address Is Null!";
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
        /// ویرایش یک شماره 
        /// </summary>
        /// <param name="Id">آیدی شماره</param>
        /// <param name="phone">شماره تلفن</param>
        /// <returns></returns>
        public Result UpdatePhone(long Id, PhoneVM phone)
        {
            Result result = new Result();
            try
            {
                Core.Domain.Phone.Phone? phonees = _phonebookDbContext.tbl_Phone.Where(a => a.Id == Id).SingleOrDefault();
                if (phonees != null)
                {
                    if (!String.IsNullOrEmpty(phone.PhoneNumber))
                        phonees.PhoneNumber = phone.PhoneNumber;
                    _phonebookDbContext.Entry(phonees).State = EntityState.Modified;
                    _phonebookDbContext.SaveChanges();
                    result.ResultStatus = true;
                    result.ResultMessage = "Success!";
                    return result;
                }
                else
                {
                    result.ResultStatus = false;
                    result.ResultMessage = "Phone Number Is Null!";
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
