using Microsoft.EntityFrameworkCore;
using Phonebook.Core.Domain.Email_Address;
using Phonebook.Core.Domain.Models;
using Phonebook.Framework.Extentions;
using Phonebook.Infra.Data.Sql.Common;

namespace Phonebook.Infra.Data.Sql.Email_Address
{
    public class efEmailRepository : IEmailRepository
    {
        private readonly PhonebookDbContext _phonebookDbContext;
        public efEmailRepository(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
        }
        /// <summary>
        /// افزودن ایمیل
        /// </summary>
        /// <param name="email">ایمیل</param>
        /// <returns></returns>
        public Result AddEmail(EmailVM email)
        {
            Result result = new Result();
            try
            {
                Core.Domain.EmailAddress.Email _Email = new Core.Domain.EmailAddress.Email();
                _Email.Value = email.Value;
                _Email.UserId = email.UserId;

                _phonebookDbContext.tbl_Email.Add(_Email);
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
        /// فراخوانی ایمیل
        /// </summary>
        /// <param name="UserId">آیدی کاربر</param>
        /// <returns></returns>
        public List<EmailVM> GetEmail(long UserId)
        {
            List<EmailVM> li = new List<EmailVM>();
            try
            {
                List<Core.Domain.EmailAddress.Email> emails = _phonebookDbContext.tbl_Email.Where(a => a.UserId == UserId).ToList();
                if (emails != null)
                {
                    foreach (var item in emails)
                    {
                        EmailVM _emailVM = new EmailVM();
                        _emailVM.Value = item.Value;
                        _emailVM.UserId = item.UserId;
                        _emailVM.Id = item.Id;
                        li.Add(_emailVM);
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
        /// فراخوانی همه ایمیل های یک مخاطب
        /// </summary>
        /// <returns></returns>
        public List<EmailVM> GetEmailes()
        {
            List<EmailVM> li = new List<EmailVM>();
            try
            {
                List<Core.Domain.EmailAddress.Email> emails = _phonebookDbContext.tbl_Email.ToList();
                if (emails != null)
                {
                    foreach (var item in emails)
                    {
                        EmailVM _emailVM = new EmailVM();
                        _emailVM.Value = item.Value;
                        _emailVM.UserId = item.UserId;
                        _emailVM.Id = item.Id;
                        li.Add(_emailVM);
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
        /// حذف یک ایمیل
        /// </summary>
        /// <param name="Id">آیدی ایمیل</param>
        /// <returns></returns>
        public Result RemoveEmail(long Id)
        {
            Result result = new Result();
            try
            {
                var emails = _phonebookDbContext.tbl_Email.Where(a => a.Id == Id).SingleOrDefault();
                if (emails != null)
                {
                    _phonebookDbContext.tbl_Email.Remove(emails);
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
        /// حذف ایمیل های یک کاربر
        /// </summary>
        /// <param name="UserId">آیدی مخاطب</param>
        /// <returns></returns>
        public Result RemoveUserEmailes(long UserId)
        {
            Result result = new Result();
            try
            {
                List<Core.Domain.EmailAddress.Email>? emailes = _phonebookDbContext.tbl_Email.Where(a => a.UserId == UserId).ToList();
                if (emailes != null)
                {
                    _phonebookDbContext.tbl_Email.RemoveRange(emailes);
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
        /// ویرایش ایمیل
        /// </summary>
        /// <param name="Id">آیدی</param>
        /// <param name="email">ایمیل</param>
        /// <returns></returns>
        public Result UpdateEmail(long Id, EmailVM email)
        {
            Result result = new Result();
            try
            {
                Core.Domain.EmailAddress.Email? emailes = _phonebookDbContext.tbl_Email.Where(a => a.Id == Id).SingleOrDefault();
                if (emailes != null)
                {
                    if (!String.IsNullOrEmpty(email.Value))
                        emailes.Value = email.Value;
                    _phonebookDbContext.Entry(emailes).State = EntityState.Modified;
                    _phonebookDbContext.SaveChanges();
                    result.ResultStatus = true;
                    result.ResultMessage = "Success!";
                    return result;
                }
                else
                {
                    result.ResultStatus = false;
                    result.ResultMessage = "Email Is Null!";
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
