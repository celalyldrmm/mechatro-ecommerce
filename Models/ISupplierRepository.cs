namespace mechatro_ecommerce.Models
{
    public interface ISupplierRepository
    {
        bool SupplierInsert(Supplier supplier);
        Task<Supplier> SupplierDetails(int? id);
        bool SupplierUpdate(Supplier supplier);
        bool SupplierDelete(int id);
        Task<List<Supplier>> SupplierSelect();
    }
}
