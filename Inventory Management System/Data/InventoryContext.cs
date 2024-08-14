using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Data
{
    internal class InventoryContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
       
        //Suppliers => table
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Inventory> Inventories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["connectionString"]);
        }

    }
}
