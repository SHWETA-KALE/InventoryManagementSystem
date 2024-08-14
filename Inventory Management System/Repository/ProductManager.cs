using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory_Management_System.Data;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Repository
{
    internal class ProductManager
    {
        private readonly InventoryContext _context;

        public ProductManager(InventoryContext context) //DI
        {
            _context = context;
        }

        public List<Product> GetAll(int id)
        {
            return _context.Products.Where(x => x.InventoryId == id).ToList();
        }


        public  int Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.ProductId;

        }
        public bool IsInventoryExists(int id)
        {
            var existingInventory = _context.Inventories.Where(x => x.InventoryId == id).FirstOrDefault();
            return existingInventory == null;
        }
        
        public Product GetByName(string name, int id)
        {
            var ExistingProduct = _context.Products.Where(x => x.ProductName == name && x.InventoryId == id).FirstOrDefault();
            return ExistingProduct; 
        }

        public bool IsProductNameExists(string name, int exludeId) { 
            return _context.Products.Any(x => x.ProductName == name && x.ProductId != exludeId);
        }

        public Product GetById(int id, int inventid)
        {
            var product = _context.Products.Where(x => x.ProductId == id && x.InventoryId==inventid).FirstOrDefault();
            if (product != null)
                _context.Entry(product).State = EntityState.Detached;
            return product;
        }

        public Product Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return product;

        }

        public void Delete(int id)
        {
            var prodToDelete = _context.Products.Where(x => x.ProductId == id).FirstOrDefault();
            if (prodToDelete != null)
            {
                _context.Products.Remove(prodToDelete);
                _context.SaveChanges();

            }

        }
    }
}
