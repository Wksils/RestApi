using Microsoft.EntityFrameworkCore;
using RestAPI.Models.Abstractions;
using RestAPI.Models.Data;

namespace RestAPI.Models.Services
{
    public class PersonService : AbstractionService, ICommonService<Person>
    {
        private readonly ApplicationContext db;

        public PersonService(ApplicationContext _db)
        {
            this.db = _db;
        }

        public bool Create(Person model)
        {
            bool result = DoAction(() =>
            {
                db.Persons.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                Person person = db.Persons.FirstOrDefault(p => p.IdPerson == id)!;
                db.Persons.Remove(person);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await db.Persons.ToListAsync();
        }

        public bool Update(int id, Person model)
        {
            bool result = DoAction(() =>
            {
                Person person = db.Persons.FirstOrDefault(p => p.IdPerson == id)!;
                person.Name = model.Name;
                person.Surname = model.Surname;
                person.Lastname = model.Lastname;
                person.Email = model.Email;
                person.PhoneNumber = model.PhoneNumber;
                person.Role = model.Role;
                db.Persons.Update(person);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<Person> Get(int id)
        {
            return await db.Persons.FindAsync(id);
        }
    }
}
