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
    internal class MainMenu
    {
        private static InventoryController InventoryController;
        public static void DisplayMenu()
        {
            InitializeInventoryController();
            while (true)
            {
                Console.WriteLine("==================WELCOME TO THE INVENTORY MANAGEMENT SYSTEM====================\n");
                Console.WriteLine("What would you like to do?\n");
                Console.WriteLine($"1.Product Management.\n" +
                    $"2.Supplier Management.\n" +
                    $"3.Transaction Management.\n" +
                    $"4.Generate Reports.\n" +
                    $"5.Exit.\n");
                Console.WriteLine("Enter your choice: ");
                int choice;
                try
                {
                     choice = Convert.ToInt32(Console.ReadLine());
                }
                catch(FormatException) {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.WriteLine();
                    continue;
                }
                
                Dotask(choice);
            }
        }

        private static void InitializeInventoryController()
        {
            var productManager = new ProductManager(new InventoryContext());
            var supplierManager = new SupplierManager(new InventoryContext());
            var transactionManager = new TransactionManager(new InventoryContext());

            
        }

        static void Dotask(int choice)
        {
            switch (choice) {
                case 1:
                    ProductController.DisplayProdMenu();
                    break;
                case 2:
                    SupplierController.DisplaySupplierMenu();
                    break;
                case 3:
                    TransactionController.DisplayTransactionMenu();
                    break;
                case 4:
                    InventoryController.GenerateReports();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.\n");
                    break;
            }
        }

        


    }
}
