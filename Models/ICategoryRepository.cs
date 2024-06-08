namespace mechatro_ecommerce.Models
{
    public interface ICategoryRepository
    {
        Task<List<Category>> CategorySelect();
        List<Category> CategorySelectMain(); 
        bool CategoryDelete(int? id); 
        bool CategoryInsert(Category category); 
        bool CategoryUpdate(Category category); 
        Task<Category> CategoryDetails(int? id);
    }
}
