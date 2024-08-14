using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory_Management_System.Data;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repository;

namespace Inventory_Management_System.Controllers
{
    internal class InventoryController
    {
        private static readonly InventoryManager _inventoryManager = new InventoryManager(new InventoryContext());


        public static void GenerateReports()
        {
            var inventories = _inventoryManager.GetAll();
            if (inventories.Count == 0)
            {
                Console.WriteLine("Inventory does not exist");
            }
            else
            {
                inventories.ForEach(x =>
                {
                    Console.WriteLine(x);
                    var products = x.Products;
                    if (products.Count == 0)
                    {
                        Console.WriteLine("Product does not exist");
                    }
                    else
                        products.ForEach(p => Console.WriteLine(p));

                    var suppliers = x.Suppliers;
                    if (suppliers.Count == 0)
                    {
                        Console.WriteLine("Supplier does not exist");
                    }
                    else
                        suppliers.ForEach(s => Console.WriteLine(s));

                    var trans = x.Transactions;
                    if (trans.Count == 0)
                    {
                        Console.WriteLine("Transaction does not exist");
                    }
                    else
                        trans.ForEach(t => Console.WriteLine(t));

                });
            }


            Console.WriteLine("\nReports Generated Successfully.");
            MainMenu.DisplayMenu();
        }

    }
}
