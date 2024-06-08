using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using XAct;


namespace mechatro_ecommerce.Models
{
    public class ProductRepository : IProductRepository
    {
        int subpageCount = 0;
        MechatroContext context = new MechatroContext();
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string? PhotoPath { get; set; }

        public async Task<List<Product>> ProductSelect()
        {
            List<Product> products = await context.Products.ToListAsync();
            return products;
        }
        public List<Product> ProductSelect(string mainPageName, int MainpageCount, string subPageName, int pagenumber)
        {
            subpageCount = context.Settings.FirstOrDefault(s => s.SettingID == 1).SubpageCount;
            List<Product> products;


            if (mainPageName == "Slider")
            {
                products = context.Products.Where(p => p.StatusID == 4 && p.IsActive == true).Take(MainpageCount).ToList();

            }

            else if (mainPageName == "New")
            {
                if (subPageName == "Ana")
                {
                    products = context.Products.Where(p => p.StatusID == 1 && p.IsActive == true).Take(MainpageCount).ToList();
                }
                else
                {
                    if (pagenumber == 0)
                    {
           
                        products = context.Products.Where(p => p.StatusID == 1 && p.IsActive == true).Take(subpageCount).ToList();
                    }
                    else
                    {
                        products = context.Products.Where(p => p.StatusID == 1 && p.IsActive == true).Skip(pagenumber * subpageCount).Take(subpageCount).ToList();
                      
                    }
                }
                
            }

       
            else if (mainPageName == "Featured")
            {
                if (subPageName == "Ana")
                {
                    products = context.Products.Where(p => p.StatusID == 2 && p.IsActive == true).Take(MainpageCount).ToList();
                }
                else
                {
                    if (pagenumber == 0)
                    {
                        products = context.Products.Where(p => p.StatusID == 2 && p.IsActive == true).Take(subpageCount).ToList();
                    }
                    else
                    {
                        products = context.Products.Where(p => p.StatusID == 2 && p.IsActive == true).Skip(pagenumber * subpageCount).Take(subpageCount).ToList();
                    }
                }
                
            }

            else if (mainPageName == "TopsellerProducts")
            {
                if (subPageName == "Ana")
                {
                    products = context.Products.Where(p => p.StatusID == 3 && p.IsActive == true).Take(MainpageCount).ToList();
                }
                else
                {
                    if (pagenumber == 0)
                    {
                        products = context.Products.Where(p => p.StatusID == 3 && p.IsActive == true).Take(subpageCount).ToList();
                    }
                    else
                    {
                        products = context.Products.Where(p => p.StatusID == 3 && p.IsActive == true).Skip(pagenumber * subpageCount).Take(subpageCount).ToList();
                    }
                }

            }

            else
            {
                products = context.Products.ToList();
            }

            return products;
        }






        public  bool ProductInsert(Product product)
        {
            try
            {
                using (MechatroContext context = new MechatroContext())
                {
                    product.AddDate = DateTime.Now;
                    context.Add(product);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public  bool ProductUpdate(Product product)
        {
            try
            {
                using (MechatroContext context = new MechatroContext())
                {
                    context.Update(product);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<Product> ProductDetails(int? id)
        {
            Product? product = await context.Products.FindAsync(id);
            return product;
        }
        public Product ProductDetails(string mainPageName)
        {
            Product? product = context.Products.FirstOrDefault(p => p.StatusID == 6);
            return product;
        }
        public  bool ProductDelete(int id)
        {
            try
            {
                using (MechatroContext context = new MechatroContext())
                {
                    Product product = context.Products.FirstOrDefault(c => c.ProductID == id);
                    product.IsActive = false;

                    List<Product> productList = context.Products.Where(c => c.ProductID == id).ToList();
                    foreach (var item in productList)
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
        
        public List<Product> ProductSelectWithCategoryID(int id)
        {
            List<Product> products = context.Products.Where(p => p.CategoryID == id).OrderBy(p => p.ProductName).ToList();
            return products;
        }
        public List<Product> ProductSelectWithSupplierID(int id)
        {
            List<Product> products = context.Products.Where(p => p.SupplierID == id).OrderBy(p => p.ProductName).ToList();
            return products;
        }

        public List<Product> SelectProductsByDetails(string query)
        {
            List<Product> products = new List<Product>();
            SqlConnection sqlConnection = Connection.ServerConnect;
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {

                Product product = new Product();
                product.ProductID = Convert.ToInt32(sqlDataReader["ProductID"]);
                product.ProductName = sqlDataReader["ProductName"].ToString();
                product.UnitPrice = Convert.ToDecimal(sqlDataReader["UnitPrice"]);
                product.PhotoPath = sqlDataReader["PhotoPath"].ToString();
                products.Add(product);
            }
            return products;
        }

       
    }
}
