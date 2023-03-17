using Microsoft.EntityFrameworkCore;
using Phonebook.Core.Domain.Address;
using Phonebook.Core.Domain.Models;
using Phonebook.Core.Domain.Person;
using Phonebook.Framework.Extentions;
using Phonebook.Infra.Data.Sql.Common;

namespace Phonebook.Infra.Data.Sql.Address
{
    public class efAddressRepository:IAddressRepository
    {
        private readonly PhonebookDbContext _phonebookDbContext;
        Core.Domain.Address.Address? _Address;
        Result result = new Result();
        AddressVM? _addressVM;
        List<AddressVM>? li;
        public efAddressRepository(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
        }

        public Result AddAddress(AddressVM address)
        {
            try
            {
                _Address = new Core.Domain.Address.Address();
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

        public List<AddressVM> GetAddress(long UserId)
        {
            try
            {
                List<Core.Domain.Address.Address> address = _phonebookDbContext.tbl_Addresses.Where(a => a.UserId == UserId).ToList();
                if (address != null)
                {
                    li = new List<AddressVM>();
                    foreach (var item in address)
                    {
                    _addressVM = new AddressVM();
                        _addressVM.Value = item.Value;
                        _addressVM.UserId = item.UserId;
                        _addressVM.Id = item.Id;
                        li.Add(_addressVM);
                    }
                    return li;
                }
                _addressVM = new AddressVM();
                return li;
            }
            catch
            {
                _addressVM = new AddressVM();
                return li;
            }
        }
        public List<AddressVM> GetAddresses()
        {
            try
            {
                List<Core.Domain.Address.Address> address = _phonebookDbContext.tbl_Addresses.ToList();
                if (address != null)
                {
                    li = new List<AddressVM>();
                    foreach (var item in address)
                    {
                    _addressVM = new AddressVM();
                        _addressVM.Value = item.Value;
                        _addressVM.UserId = item.UserId;
                        _addressVM.Id = item.Id;
                        li.Add(_addressVM);
                    }
                    return li;
                }
                _addressVM = new AddressVM();
                return li;
            }
            catch
            {
                _addressVM = new AddressVM();
                return li;
            }
        }


        public Result RemoveAddress(long Id)
        {
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

        public Result RemoveUserAddresses(long UserId)
        {
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

        public Result UpdateAddress(long Id, AddressVM Address)
        {
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
