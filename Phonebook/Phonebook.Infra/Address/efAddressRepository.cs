using Microsoft.EntityFrameworkCore;
using Phonebook.Core.Domain.Address;
using Phonebook.Core.Domain.Models;
using Phonebook.Core.Domain.Person;
using Phonebook.Framework.Extentions;
using Phonebook.Infra.Data.Sql.Common;

namespace Phonebook.Infra.Data.Sql.Address
{
    public class efAddressRepository : IAddressRepository
    {
        private readonly PhonebookDbContext _phonebookDbContext;
        public efAddressRepository(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
        }
        /// <summary>
        /// افزودن آدرس
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Result AddAddress(AddressVM address)
        {
            Result result = new Result();
            try
            {
                Core.Domain.Address.Address _Address = new Core.Domain.Address.Address();
                _Address.Value = address.Value;
                _Address.UserId = address.UserId;

                _phonebookDbContext.tbl_Addresses.Add(_Address);
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
        /// فراخوانی آدرس
        /// </summary>
        /// <param name="UserId">آیدی مخاطب</param>
        /// <returns></returns>
        public List<AddressVM> GetAddress(long UserId)
        {
            List<AddressVM> li = new List<AddressVM>();
            try
            {
                List<Core.Domain.Address.Address> address = _phonebookDbContext.tbl_Addresses.Where(a => a.UserId == UserId).ToList();
                if (address != null)
                {
                    foreach (var item in address)
                    {
                        AddressVM _addressVM = new AddressVM();
                        _addressVM.Value = item.Value;
                        _addressVM.UserId = item.UserId;
                        _addressVM.Id = item.Id;
                        li.Add(_addressVM);
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
        /// فراخوانی همه آدرس ها
        /// </summary>
        /// <returns></returns>
        public List<AddressVM> GetAddresses()
        {
            List<AddressVM> li = new List<AddressVM>();
            try
            {
                List<Core.Domain.Address.Address> address = _phonebookDbContext.tbl_Addresses.ToList();
                if (address != null)
                {
                    foreach (var item in address)
                    {
                        AddressVM _addressVM = new AddressVM();
                        _addressVM.Value = item.Value;
                        _addressVM.UserId = item.UserId;
                        _addressVM.Id = item.Id;
                        li.Add(_addressVM);
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
        /// حذف یک آدرس
        /// </summary>
        /// <param name="Id">آیدی</param>
        /// <returns></returns>
        public Result RemoveAddress(long Id)
        {
            Result result = new Result();
            try
            {
                var address = _phonebookDbContext.tbl_Addresses.Where(a => a.Id == Id).SingleOrDefault();
                if (address != null)
                {
                    _phonebookDbContext.tbl_Addresses.Remove(address);
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
        /// حذف آدرس های یک مخاطب
        /// </summary>
        /// <param name="UserId">آیدی مخاطب</param>
        /// <returns></returns>
        public Result RemoveUserAddresses(long UserId)
        {
            Result result = new Result();
            try
            {
                List<Core.Domain.Address.Address>? address = _phonebookDbContext.tbl_Addresses.Where(a => a.UserId == UserId).ToList();
                if (address != null)
                {
                    _phonebookDbContext.tbl_Addresses.RemoveRange(address);
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
        /// ویرایش آدرس
        /// </summary>
        /// <param name="Id">آیدی</param>
        /// <param name="Address">اطلاعات ویرایش شده</param>
        /// <returns></returns>
        public Result UpdateAddress(long Id, AddressVM Address)
        {
            Result result = new Result();
            try
            {
                Core.Domain.Address.Address? address = _phonebookDbContext.tbl_Addresses.Where(a => a.Id == Id).SingleOrDefault();
                if (address != null)
                {
                    if (!String.IsNullOrEmpty(Address.Value))
                        address.Value = Address.Value;
                    _phonebookDbContext.Entry(address).State = EntityState.Modified;
                    _phonebookDbContext.SaveChanges();
                    result.ResultStatus = true;
                    result.ResultMessage = "Success!";
                    return result;
                }
                else
                {
                    result.ResultStatus = false;
                    result.ResultMessage = "Person Is Null!";
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
