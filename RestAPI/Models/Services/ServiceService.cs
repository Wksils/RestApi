using Microsoft.EntityFrameworkCore;
using RestAPI.Models.Abstractions;
using RestAPI.Models.Data;

namespace RestAPI.Models.Services
{
    public class ServiceService:AbstractionService, ICommonService<Service>
    {
        private readonly ApplicationContext db;

        public ServiceService(ApplicationContext _db)
        {
            this.db = _db;
        }

        public bool Create(Service model)
        {
            bool result = DoAction(() =>
            {
                db.Services.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                Service service = db.Services.FirstOrDefault(p => p.IdService == id)!;
                db.Services.Remove(service);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            return await db.Services.ToListAsync();
        }

        public bool Update(int id, Service model)
        {
            bool result = DoAction(() =>
            {
                Service servise = db.Services.FirstOrDefault(p => p.IdService == id)!;
                servise.DateService = model.DateService;
                servise.DescriptionService = model.DescriptionService;
                servise.Problem = model.Problem;
                db.Services.Update(servise);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<Service> Get(int id)
        {
            var res = await db.Services.FirstOrDefaultAsync<Service>(p=>p.IdService == id);
            return res;
        }
    }
}
