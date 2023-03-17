using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Phonebook.Core.Domain.Contacts;
using Phonebook.Core.Domain.Models;
using Phonebook.Core.Domain.Person;
using Phonebook.Core.Domain.SignificantDate;
using Phonebook.Framework.Extentions;
using Phonebook.Infra.Data.Sql.Address;
using Phonebook.Infra.Data.Sql.Common;
using Phonebook.Infra.Data.Sql.Email_Address;
using Phonebook.Infra.Data.Sql.Person;
using Phonebook.Infra.Data.Sql.Phone;
using Phonebook.Infra.Data.Sql.Significant_Date;
using System;
using System.Security.Cryptography;

namespace Phonebook.Infra.Data.Sql.Contacts
{
    public class efContactsRepository : IContactsRepository
    {
        private readonly PhonebookDbContext _phonebookDbContext;
        Result result = new Result();
        efPersonRepository person;
        efPhoneRepository phone;
        efAddressRepository address;
        efEmailRepository email;
        efSignificantDateRepository sdr;
        public efContactsRepository(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
            person = new efPersonRepository(_phonebookDbContext);
            phone = new efPhoneRepository(_phonebookDbContext);
            address = new efAddressRepository(_phonebookDbContext);
            email = new efEmailRepository(_phonebookDbContext);
            sdr = new efSignificantDateRepository(_phonebookDbContext);
        }
        public Result AddContact(ContactsVM inputs)
        {
            using (var transaction = _phonebookDbContext.Database.BeginTransaction())
            {
                try
                {
                    long pId = person.AddPerson(inputs.PersonDetails);
                    foreach (var item in inputs.PhoneList)
                    {
                        item.UserId = pId;
                        phone.AddPhone(item);
                    }
                    if (inputs.AddressList != null)
                    {
                        foreach (var item in inputs.AddressList)
                        {
                            item.UserId = pId;
                            address.AddAddress(item);
                        }
                    }
                    if (inputs.EmailList != null)
                    {
                        foreach (var item in inputs.EmailList)
                        {
                            item.UserId = pId;
                            email.AddEmail(item);
                        }
                    }
                    if (inputs.SignificantDateList != null)
                    {
                        foreach (var item in inputs.SignificantDateList)
                        {
                            item.UserId = pId;
                            sdr.AddSignificantDate(item);
                        }
                    }

                    if (result.ResultStatus == true)
                        transaction.Commit();
                    else
                    {
                        transaction.Rollback();
                    result.ResultStatus = false;
                    result.ResultMessage = "Error!";
                    return result;
                    }
                    result.ResultStatus = true;
                    result.ResultMessage = "Contact Inserted!!!";
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result.ResultStatus = false;
                    result.ResultMessage = ex.Message;
                    return result;
                }
            }
        }

        public ContactsVM GetContact(long Id)
        {
            try
            {
                ContactsVM _contactsVM = new ContactsVM();
                _contactsVM.PersonDetails = person.GetPerson(Id);
                _contactsVM.PhoneList = phone.GetPhone(Id);
                _contactsVM.AddressList = address.GetAddress(Id);
                _contactsVM.SignificantDateList = sdr.GetSignificantDate(Id);
                _contactsVM.EmailList = email.GetEmail(Id);
                return _contactsVM;
            }
            catch
            {
                ContactsVM _contactsVM = new ContactsVM();
                return _contactsVM;
            }
        }

        public List<ContactResults> GetContacts()
        {
            ContactLists _contacts = new ContactLists();
            List<ContactResults> _contactsresultList = new List<ContactResults>();
            ContactResults _contactsresult;
            _contacts.PersonDetailsList = person.GetAllPerson();
            _contacts.PhoneList = phone.GetPhones();
            _contacts.AddressList = address.GetAddresses();
            _contacts.SignificantDateList = sdr.GetSignificantDates();
            _contacts.EmailList = email.GetEmailes();
            foreach (var item in _contacts.PersonDetailsList)
            {
                _contactsresult = new ContactResults
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    NikName = item.NikName,
                    Company = item.Company,
                    Notes = item.Notes,
                    PhoneNumberList = _contacts.PhoneList != null ? _contacts.PhoneList.Where(a => a.UserId == item.Id).ToList() : null,
                    addressList = _contacts.AddressList != null ? _contacts.AddressList.Where(a => a.UserId == item.Id).ToList() : null,
                    EmailValueList = _contacts.EmailList != null ? _contacts.EmailList.Where(a => a.UserId == item.Id).ToList() : null,
                    significantDateList = _contacts.SignificantDateList != null ? _contacts.SignificantDateList.Where(a => a.UserId == item.Id).ToList() : null
                };
                _contactsresultList.Add(_contactsresult);
            }
            return _contactsresultList;
        }

        public Result RemoveContact(long Id)
        {
            using (var transaction = _phonebookDbContext.Database.BeginTransaction())
            {
                try
                {
                    ContactsVM res = GetContact(Id);
                    if (res.PersonDetails != null)
                        result = person.RemovePerson(Id);
                    else
                    {
                        result.ResultStatus = false;
                        result.ResultMessage = "Not Found!!!";
                        return result;
                    }
                    if (result.ResultStatus == true)
                    {
                        if (res.PhoneList != null)
                        {
                            result=phone.RemoveUserPhonees(Id);
                        }
                        else
                        {
                            result.ResultStatus = false;
                            result.ResultMessage = "Not Found!!!";
                            return result;
                        }
                    }
                    if (result.ResultStatus == true)
                    {
                        if (res.AddressList != null)
                        {
                            result=address.RemoveUserAddresses(Id);
                        }
                        else
                        {
                            result.ResultStatus = false;
                            result.ResultMessage = "Not Found!!!";
                            return result;
                        }
                    }
                    if (result.ResultStatus == true)
                    {
                        if (res.EmailList != null)
                        {
                            result=email.RemoveUserEmailes(Id);
                        }
                        else
                        {
                            result.ResultStatus = false;
                            result.ResultMessage = "Not Found!!!";
                            return result;
                        }
                    }
                    if (result.ResultStatus == true)
                    {
                        if (res.SignificantDateList != null)
                        {
                            result=sdr.RemoveUserSignificantDates(Id);
                        }
                        else
                        {
                            result.ResultStatus = false;
                            result.ResultMessage = "Not Found!!!";
                            return result;
                        }
                    }
                    if (result.ResultStatus == true)
                        transaction.Commit();
                    else
                    {
                        transaction.Rollback();
                        result.ResultStatus = false;
                        result.ResultMessage = "Error!";
                        return result;
                    }
                    result.ResultStatus = true;
                    result.ResultMessage = "Contact Deleted!!!";
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result.ResultStatus = false;
                    result.ResultMessage = ex.Message;
                    return result;
                }
            }
        }

        public Result UpdateContact(long Id, ContactsVM inputs)
        {
            using (var transaction = _phonebookDbContext.Database.BeginTransaction())
            {
                try
                {
                    ContactsVM res = GetContact(Id);
                    if (res.PersonDetails != null)
                        result = person.UpdatePerson(Id, inputs.PersonDetails);
                    else
                    {
                        result.ResultStatus = false;
                        result.ResultMessage = "Not Found!!!";
                        return result;
                    }
                    if (result.ResultStatus == true)
                    {
                        if (res.PhoneList != null)
                        {
                            foreach (var item in inputs.PhoneList)
                                result = phone.UpdatePhone(item.Id, item);
                        }
                        else
                        {
                            result.ResultStatus = false;
                            result.ResultMessage = "Not Found!!!";
                            return result;
                        }
                    }
                    if (result.ResultStatus == true)
                    {
                        if (res.AddressList != null)
                        {
                            foreach (var item in inputs.AddressList)
                                result = address.UpdateAddress(item.Id, item);
                        }
                        else
                        {
                            result.ResultStatus = false;
                            result.ResultMessage = "Not Found!!!";
                            return result;
                        }
                    }
                    if (result.ResultStatus == true)
                    {
                        if (res.EmailList != null)
                        {
                            foreach (var item in inputs.EmailList)
                                result = email.UpdateEmail(item.Id, item);
                        }
                        else
                        {
                            result.ResultStatus = false;
                            result.ResultMessage = "Not Found!!!";
                            return result;
                        }
                    }
                    if (result.ResultStatus == true)
                    {
                        if (res.SignificantDateList != null)
                        {
                            foreach (var item in inputs.SignificantDateList)
                                result = sdr.UpdateSignificantDate(item.Id, item);
                        }
                        else
                        {
                            result.ResultStatus = false;
                            result.ResultMessage = "Not Found!!!";
                            return result;
                        }
                    }
                    if (result.ResultStatus == true)
                        transaction.Commit();
                    else
                    {
                        transaction.Rollback();
                        result.ResultStatus = false;
                        result.ResultMessage = "Error!";
                        return result;
                    }
                    result.ResultStatus = true;
                    result.ResultMessage = "Contact Updated!!!";
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result.ResultStatus = false;
                    result.ResultMessage = ex.Message;
                    return result;
                }
            }
        }
    }
}
