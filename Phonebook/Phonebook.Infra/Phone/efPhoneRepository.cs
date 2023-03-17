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
        Core.Domain.Phone.Phone? _Phone;
        Result result = new Result();
        PhoneVM? _phoneVM;
        List<PhoneVM>? li;
        public efPhoneRepository(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
        }
        public Result AddPhone(PhoneVM phone)
        {
            try
            {
                _Phone = new Core.Domain.Phone.Phone();
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

        public List<PhoneVM> GetPhone(long UserId)
        {
            try
            {
                List<Core.Domain.Phone.Phone> phones = _phonebookDbContext.tbl_Phone.Where(a => a.UserId == UserId).ToList();
                if (phones != null)
                {
                    li = new List<PhoneVM>();
                    foreach (var item in phones)
                    {
                        _phoneVM = new PhoneVM();
                        _phoneVM.PhoneNumber = item.PhoneNumber;
                        _phoneVM.UserId = item.UserId;
                        _phoneVM.Id = item.Id;
                        li.Add(_phoneVM);
                    }
                    return li;
                }
                _phoneVM = new PhoneVM();
                return li;
            }
            catch
            {
                _phoneVM = new PhoneVM();
                return li;
            }
        }
        public List<PhoneVM> GetPhones()
        {
            try
            {
                List<Core.Domain.Phone.Phone> phones = _phonebookDbContext.tbl_Phone.ToList();
                if (phones != null)
                {
                    li = new List<PhoneVM>();
                    foreach (var item in phones)
                    {
                        _phoneVM = new PhoneVM();
                        _phoneVM.PhoneNumber = item.PhoneNumber;
                        _phoneVM.UserId = item.UserId;
                        _phoneVM.Id = item.Id;
                        li.Add(_phoneVM);
                    }
                    return li;
                }
                _phoneVM = new PhoneVM();
                return li;
            }
            catch
            {
                _phoneVM = new PhoneVM();
                return li;
            }
        }

        public Result RemovePhone(long Id)
        {
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

        public Result RemoveUserPhonees(long UserId)
        {
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

        public Result UpdatePhone(long Id, PhoneVM phone)
        {
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
