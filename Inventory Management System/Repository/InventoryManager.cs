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
    internal class InventoryManager
    {
        private readonly InventoryContext _context;

        public InventoryManager(InventoryContext context)
        {
            _context = context;
        }
        public List<Inventory> GetAll()
        {
            //        return _employeeContext.Departments.Include(department => department.Employees)
            //.ThenInclude(employee => employee.Workstation)
            //.ToList();

            return _context.Inventories.Include(inventory => inventory.Products)
                .Include(inventory => inventory.Suppliers)
                .Include(inventory => inventory.Transactions)
                .ToList();
        }
    }
}
