using Microsoft.EntityFrameworkCore;
using RestAPI.Models.Abstractions;
using RestAPI.Models.Data;
using System.Security.Claims;
using System.Text;

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
            var res = await db.Persons.FirstOrDefaultAsync<Person>(p=>p.IdPerson == id);
            return res;
        }
        public Tuple<string,string> GetUserLoginFromBasicAuth(HttpRequest request)
        {
            string userName = "";
            string userPass = "";
            string authHeader = request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUserNamePass = authHeader.Replace("Basic ", "");
                var encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
                string[] namePassArray=encoding.GetString(Convert.FromBase64String(encodedUserNamePass)).Split(':');
                userName = namePassArray[0];
                userPass = namePassArray[1];
            }
            return new Tuple<string,string>(userName, userPass);
        }
        public Person GetUser(string login)
        {
            var user = db.Persons.FirstOrDefault(u => u.Email == login);
            return user!;
        }
        public Person GetUser(string login, string password)
        {
            var user = db.Persons.FirstOrDefault(u => u.Email == login && u.Password == password);
            return user!;
        }
        public ClaimsIdentity GetIdentity(string username, string password)
        {
            Person person = GetUser(username, password);
            if (person != null)
            {
                var cliims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,person.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,person.Role)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(cliims, "Token",
                    ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null!;
        }
    }
}
