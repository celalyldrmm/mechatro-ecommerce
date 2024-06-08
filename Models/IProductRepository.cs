namespace mechatro_ecommerce.Models
{
    public interface IProductRepository
    {
              
        Task<List<Product>> ProductSelect();
        List<Product> ProductSelect(string mainPageName, int MainpageCount, string subPageName, int pagenumber);
        bool ProductInsert(Product product);
        bool ProductUpdate(Product product);
        bool ProductDelete(int id);
        Task<Product> ProductDetails(int? id);
        Product ProductDetails(string mainPageName);
       
        List<Product> ProductSelectWithCategoryID(int id);
        List<Product> ProductSelectWithSupplierID(int id);
        List<Product> SelectProductsByDetails(string query);
      

    }
}
