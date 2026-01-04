using RestAPI.Models.Abstractions;
using RestAPI.Models.Data;

namespace RestAPI.Models.Services
{
    public class SaleService:AbstractionService, ICommonService<Sale>
    {
        private readonly ApplicationContext db;

        public SaleService(ApplicationContext _db)
        {
            this.db = _db;
        }

        public bool Create(Sale model)
        {
            bool result = DoAction(() =>
            {
                db.Sales.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                Sale sale = db.Sales.FirstOrDefault(p => p.IdSales == id)!;
                db.Sales.Remove(sale);
                db.SaveChanges();
            });
            return result;
        }

        public Sale Get(int id)
        {
            Sale sale = db.Sales.FirstOrDefault(p => p.IdSales == id)!;
            if (sale != null) return sale;
            return null!;
        }

        public bool Update(int id, Sale model)
        {
            bool result = DoAction(() =>
            {
                Sale sale = db.Sales.FirstOrDefault(p => p.IdSales == id)!;
                sale.SoldProduct = model.SoldProduct;
                sale.SummaSale = model.SummaSale;
                sale.DateTimeSale = model.DateTimeSale;
                sale.PaymentMethod = model.PaymentMethod;
                db.Sales.Update(sale);
                db.SaveChanges();
            });
            return result;
        }
    }
}
