namespace mechatro_ecommerce.Models
{
    public class MainPageModel
    {
        public List<Product?> SliderProducts { get; set; }
        public List<Product>? NewProducts { get; set; }
        public List<Product>? FeaturedProducts { get; set; }
        public List<Product>? TopsellerProducts { get; set; } 
        public List<vw_MyOrders>? MyOrders { get; set; }



        // For Products Details;
        public Product? ProductDetails { get; set; }
        public List<Category>? CategoryDetails { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public List<Product>? RealetedProducts { get; set; }
    }
}
