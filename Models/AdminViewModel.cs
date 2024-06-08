using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mechatro_ecommerce.Models;

namespace mechatro_ecommerce.Models
{
	
	public class AdminViewModel
	{
        public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Status> Statuses { get; set; }

    }
}
