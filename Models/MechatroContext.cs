﻿using mechatro_ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace mechatro_ecommerce.Models
{
    public class MechatroContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration["ConnectionString:MechatroProject"]);

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }        
        public DbSet<Setting> Settings { get; set; }
        public DbSet<vw_MyOrders> vw_MyOrders { get; set; }
        public DbSet<SP_Search> sp_Searches { get; set; }










    }


}
