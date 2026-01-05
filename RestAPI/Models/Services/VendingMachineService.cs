using Microsoft.EntityFrameworkCore;
using RestAPI.Models.Abstractions;
using RestAPI.Models.Data;

namespace RestAPI.Models.Services
{
    public class VendingMachineService:AbstractionService, ICommonService<VendingMachine>
    {
        private readonly ApplicationContext db;

        public VendingMachineService(ApplicationContext _db)
        {
            this.db = _db;
        }

        public bool Create(VendingMachine model)
        {
            bool result = DoAction(() =>
            {
                db.VendingMachines.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                VendingMachine vendingMachine = db.VendingMachines.FirstOrDefault(p => p.IdVm == id)!;
                db.VendingMachines.Remove(vendingMachine);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<IEnumerable<VendingMachine>> GetAll()
        {
            return await db.VendingMachines.ToListAsync();
        }

        public bool Update(int id, VendingMachine model)
        {
            bool result = DoAction(() =>
            {
                VendingMachine vendingMachine = db.VendingMachines.FirstOrDefault(p => p.IdVm == id)!;
                vendingMachine.Adres = model.Adres;
                vendingMachine.Model = model.Model;
                vendingMachine.Type = model.Type;
                vendingMachine.Summa = model.Summa;
                vendingMachine.Number = model.Number;
                vendingMachine.InventoryNumber = model.InventoryNumber;
                vendingMachine.DateOfManufacture = model.DateOfManufacture;
                vendingMachine.DateOfOperation = model.DateOfOperation;
                vendingMachine.DataLastExamination = model.DataLastExamination;
                vendingMachine.ResoursTa = model.ResoursTa;
                vendingMachine.DateNextExamination = model.DataLastExamination;
                vendingMachine.TimeExamination = model.TimeExamination;
                vendingMachine.Status = model.Status;
                vendingMachine.DateInventory = model.DateInventory;
                db.VendingMachines.Update(vendingMachine);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<VendingMachine> Get(int id)
        {
            return await db.VendingMachines.FindAsync(id);
        }
    }
}
