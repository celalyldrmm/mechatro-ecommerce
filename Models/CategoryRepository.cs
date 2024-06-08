using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mechatro_ecommerce.Models
{
    public class CategoryRepository : ICategoryRepository
    {
		MechatroContext context = new MechatroContext();


        public async Task<List<Category>> CategorySelect()
        {
            List<Category> categories = await context.Categories.ToListAsync();
            return categories;
        }
        public List<Category> CategorySelectMain()
        {
            List<Category> categories = context.Categories.Where(c => c.ParentID == 0).ToList();
            return categories;
        }
        public  bool CategoryInsert(Category category)
        {
            try
            {
                using (MechatroContext context = new MechatroContext())
                {
                    context.Add(category);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<Category> CategoryDetails(int? id)
        {
            Category? categories = await context.Categories.FindAsync(id);
            return categories;
        }
        public  bool CategoryUpdate(Category category)
        {
            try
            {
                using (MechatroContext context = new MechatroContext())
                {
                    context.Update(category);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public  bool CategoryDelete(int? id)
        {
            try
            {
                using (MechatroContext context = new MechatroContext())
                {
                    Category category = context.Categories.FirstOrDefault(c => c.CategoryID == id);
                    category.IsActive = false;

                    List<Category> categoryList = context.Categories.Where(c => c.ParentID == id).ToList();
                    foreach (var item in categoryList)
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
