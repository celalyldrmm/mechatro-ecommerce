using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace mechatro_ecommerce.Models
{
    public class SupplierRepository : ISupplierRepository
    {
        MechatroContext context = new MechatroContext();
        public async Task<List<Supplier>> SupplierSelect()
        {
            List<Supplier> suppliers = await context.Suppliers.ToListAsync();
            return suppliers;
        }
        public  bool SupplierInsert(Supplier supplier)
        {
            try
            {
                using (MechatroContext context = new MechatroContext())
                {
                    context.Add(supplier);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<Supplier> SupplierDetails(int? id)
        {
            Supplier? supplier = await context.Suppliers.FirstOrDefaultAsync(S => S.SupplierID == id);
            return supplier;
        }
        public  bool SupplierUpdate(Supplier supplier)
        {
            try
            {
                using (MechatroContext context = new MechatroContext())
                {
                    context.Update(supplier);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public  bool SupplierDelete(int id)
        {
            try
            {
                using (MechatroContext context = new MechatroContext())
                {
                    Supplier supplier = context.Suppliers.FirstOrDefault(c => c.SupplierID == id);
                    supplier.IsActive = false;

                    List<Supplier> supplierList = context.Suppliers.Where(c => c.SupplierID == id).ToList();
                    foreach (var item in supplierList)
                    {
                        item.IsActive = false;
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
