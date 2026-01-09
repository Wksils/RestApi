using Microsoft.EntityFrameworkCore;
using RestAPI.Models.Abstractions;
using RestAPI.Models.Data;

namespace RestAPI.Models.Services
{
    public class GoodService : AbstractionService, ICommonService<Good>
    {
        private readonly ApplicationContext db;

        public GoodService(ApplicationContext _db)
        {
            this.db = _db;
        }

        public bool Create(Good model)
        {
           bool result = DoAction(delegate ()
           {
               db.Goods.Add(model);
               db.SaveChanges();
           });
           return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                Good good = db.Goods.FirstOrDefault(p => p.IdProduct==id)!;
                db.Goods.Remove(good);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<Good> Get(int id)
        {
            var res= await db.Goods.FirstOrDefaultAsync<Good>(p=>p.IdProduct==id);
            return res!;
        }

        public async Task<IEnumerable<Good>> GetAll()
        {
            return await db.Goods.ToListAsync();
        }

        public bool Update(int id, Good model)
        {
            bool result = DoAction(delegate ()
            {
                Good good = db.Goods.FirstOrDefault(p => p.IdProduct == id)!;
                good.Price= model.Price;
                good.Sales = model.Sales;
                good.Description = model.Description;
                good.MinimumStock = model.MinimumStock;
                good.Quantity = model.Quantity;
                good.ProductName = model.ProductName;
                db.Goods.Update(good);
                db.SaveChanges();
            });
            return result;
        }
    }
}
