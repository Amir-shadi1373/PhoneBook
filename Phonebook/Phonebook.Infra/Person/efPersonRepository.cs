using Microsoft.EntityFrameworkCore;
using Phonebook.Core.Domain.Person;
using Phonebook.Framework.Extentions;
using Phonebook.Infra.Data.Sql.Common;

namespace Phonebook.Infra.Data.Sql.Person
{
    public class efPersonRepository : IPersonRepository
    {
        private readonly PhonebookDbContext _phonebookDbContext;
        Phonebook.Core.Domain.Person.Person _person;
        PersonVM _personVM;
        List<PersonVM> li;
        Result result = new Result();
        public efPersonRepository(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
        }

        public long AddPerson(PersonVM person)
        {
            try
            {
                _person = new Core.Domain.Person.Person();
                _person.FirstName = person.FirstName;
                _person.LastName = person.LastName;
                _person.Notes = person.Notes;
                _person.UserPicture = person.UserPicture;
                _person.NikName = person.NikName;
                _person.Company = person.Company;

                _phonebookDbContext.tbl_Person.Add(_person);
                _phonebookDbContext.SaveChanges();

                Core.Domain.Person.Person? existItem = _phonebookDbContext.tbl_Person.OrderBy(b=>b.Id).LastOrDefault();
                if (existItem != null)
                return existItem.Id;
                else
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public List<PersonVM> GetAllPerson()
        {
            try
            {
                List<Core.Domain.Person.Person> people = _phonebookDbContext.tbl_Person.ToList();
                li = new List<PersonVM>();
                foreach (var item in people)
                {
                    _personVM = new PersonVM();
                    _personVM.FirstName = item.FirstName;
                    _personVM.Notes = item.Notes;
                    _personVM.UserPicture = item.UserPicture;
                    _personVM.Company = item.Company;
                    _personVM.LastName = item.LastName;
                    _personVM.NikName = item.NikName;
                    _personVM.Id = item.Id;
                    li.Add(_personVM);
                }
                return li;
            }
            catch
            {
                li = new List<PersonVM>();
                return li;
            }
        }

        public PersonVM GetPerson(long Id)
        {
            try
            {
                var people = _phonebookDbContext.tbl_Person.Where(a => a.Id == Id).SingleOrDefault();
                if (people != null)
                {
                    _personVM = new PersonVM();
                    _personVM.FirstName = people.FirstName;
                    _personVM.Notes = people.Notes;
                    _personVM.UserPicture = people.UserPicture;
                    _personVM.Company = people.Company;
                    _personVM.LastName = people.LastName;
                    _personVM.NikName = people.NikName;
                    _personVM.Id = people.Id;
                    return _personVM;
                }
                _personVM = new PersonVM();
                return _personVM;
            }
            catch (Exception)
            {
                _personVM = new PersonVM();
                return _personVM;
            }
        }

        public Result RemovePerson(long Id)
        {
            try
            {
                var people = _phonebookDbContext.tbl_Person.Where(a => a.Id == Id).SingleOrDefault();
                if (people != null)
                {
                    _phonebookDbContext.tbl_Person.Remove(people);
                    _phonebookDbContext.SaveChanges();
                    result.ResultStatus = true;
                    result.ResultMessage = "success!";
                    return result;
                }
                result.ResultStatus = false;
                result.ResultMessage = "Person Is Null!";
                return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = false;
                result.ResultMessage = ex.Message;
                return result;
            }
        }

        public Result UpdatePerson(long Id, PersonVM person)
        {
            try
            {
                Core.Domain.Person.Person? people = _phonebookDbContext.tbl_Person.Where(a => a.Id == Id).SingleOrDefault();
                if (people != null)
                { 
                    if(!String.IsNullOrEmpty(person.FirstName))
                    people.FirstName = person.FirstName;
                    if(!String.IsNullOrEmpty(person.LastName))
                    people.LastName = person.LastName;
                    if(!String.IsNullOrEmpty(person.Notes))
                    people.Notes = person.Notes;
                    if(!String.IsNullOrEmpty(person.UserPicture))
                    people.UserPicture = person.UserPicture;
                    if(!String.IsNullOrEmpty(person.NikName))
                    people.NikName = person.NikName;
                    if(!String.IsNullOrEmpty(person.Company))
                    people.Company = person.Company;
                    _phonebookDbContext.Entry(people).State = EntityState.Modified;
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
