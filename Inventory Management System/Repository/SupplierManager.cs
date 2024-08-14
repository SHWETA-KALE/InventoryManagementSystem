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
    internal class SupplierManager
    {
        private readonly InventoryContext _context;

        public SupplierManager(InventoryContext context)
        {
            _context = context;
        }

        public List<Supplier> GetAll(int id)
        {
            return _context.Suppliers.Where(x => x.InventoryId == id).ToList();
        }
        public bool IsInventoryExists(int id)
        {
            var existingInventory = _context.Inventories.Where(x => x.InventoryId == id).FirstOrDefault();
            return existingInventory == null;
        }
        public Supplier GetByName(string name, int inventid)
        {
            var existingSupplier = _context.Suppliers.Where(x => x.SupplierName == name && x.InventoryId == inventid).FirstOrDefault();
            return existingSupplier;
        }

        public Supplier GetById(int id)
        {
            var supplier = _context.Suppliers.Where(x => x.SupplierId == id).FirstOrDefault();
            if (supplier != null)
                _context.Entry(supplier).State = EntityState.Detached;
            return supplier;
        }

        public bool IsSupplierExists(string supplierName, int excludeid)
        {
            return _context.Suppliers.Any(x=>x.SupplierName==supplierName &&  x.SupplierId!=excludeid);
        }
        

        public void Add(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

        }

        public void Update(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            _context.SaveChanges();
        }

        public string Delete(int id)
        {
            var supplierToDelete = _context.Suppliers.Where(x => x.SupplierId == id).FirstOrDefault();
            if (supplierToDelete != null)
            {
                _context.Suppliers.Remove(supplierToDelete);
                _context.SaveChanges();
                return "Supplier Deleted Successfully\n";
            }
            return "Supplier does not exist";
        }
    }
}
