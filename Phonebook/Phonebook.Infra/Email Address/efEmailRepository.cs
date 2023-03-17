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
        Core.Domain.EmailAddress.Email? _Email;
        Result result = new Result();
        EmailVM? _emailVM;
        List<EmailVM>? li;
        public efEmailRepository(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
        }
        public Result AddEmail(EmailVM email)
        {
            try
            {
                _Email = new Core.Domain.EmailAddress.Email();
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

        public List<EmailVM> GetEmail(long UserId)
        {
            try
            {
                List<Core.Domain.EmailAddress.Email> emails = _phonebookDbContext.tbl_Email.Where(a => a.UserId == UserId).ToList();
                if (emails != null)
                {
                    li = new List<EmailVM>();
                    foreach (var item in emails)
                    {
                        _emailVM = new EmailVM();
                        _emailVM.Value = item.Value;
                        _emailVM.UserId = item.UserId;
                        _emailVM.Id = item.Id;
                        li.Add(_emailVM);
                    }
                    return li;
                }
                _emailVM = new EmailVM();
                return li;
            }
            catch
            {
                _emailVM = new EmailVM();
                return li;
            }
        }
        public List<EmailVM> GetEmailes()
        {
            try
            {
                List<Core.Domain.EmailAddress.Email> emails = _phonebookDbContext.tbl_Email.ToList();
                if (emails != null)
                {
                    li = new List<EmailVM>();
                    foreach (var item in emails)
                    {
                        _emailVM = new EmailVM();
                        _emailVM.Value = item.Value;
                        _emailVM.UserId = item.UserId;
                        _emailVM.Id = item.Id;
                        li.Add(_emailVM);
                    }
                    return li;
                }
                _emailVM = new EmailVM();
                return li;
            }
            catch
            {
                _emailVM = new EmailVM();
                return li;
            }
        }

        public Result RemoveEmail(long Id)
        {
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

        public Result RemoveUserEmailes(long UserId)
        {
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

        public Result UpdateEmail(long Id, EmailVM email)
        {
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
