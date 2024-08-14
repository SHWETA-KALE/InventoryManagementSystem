using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Inventory_Management_System.Data;
using Inventory_Management_System.Exceptions;
using Inventory_Management_System.Migrations;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repository;

namespace Inventory_Management_System.Controllers
{

    internal class SupplierController
    {
        private readonly static SupplierManager _supplierManager = new SupplierManager(new InventoryContext());

        public static void DisplaySupplierMenu()
        {
            Console.WriteLine("===========================SUPPLIER MANAGEMENT===========================");
            while (true)
            {
                Console.WriteLine($"1.Add Supplier\n" +
                    $"2.Update Supplier\n" +
                    $"3.Delete Supplier\n" +
                    $"4.View Supplier Details\n" +
                    $"5. View All Suppliers\n" +
                    $"6.Exit\n" +
                    $"7. Go back to main menu\n");
                int choice;
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.WriteLine();
                    continue;
                }
                try
                {
                    DoTask(choice);
                    Console.WriteLine(".......................................................................................................");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter numbers only!\n");
                }
                
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        static void DoTask(int choice)
        {
            //var context = new InventoryContext();
            //var supplierManager = new SupplierManager(context)
            switch (choice)
            {
                case 1:
                    AddSupplier();
                    break;
                case 2:
                    UpdateSupplier();
                    break;
                case 3:
                    DeleteSupplier();
                    break;
                case 4:
                    ViewSupplierDetails();
                    break;
                case 5:
                    GetAllSuppliers();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                case 7:
                    MainMenu.DisplayMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.\n");
                    break;
            }
        }
        public static void AddSupplier()
        {
            Console.WriteLine("Enter InventoryId: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());
            //checking if inventory exists
            if (_supplierManager.IsInventoryExists(inventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exist");
            }
            Console.WriteLine("Enter Supplier Name: ");
            string supplierName = Console.ReadLine();
            var existingSupplier = _supplierManager.GetByName(supplierName, inventoryId);

            if (existingSupplier != null)
            {
                throw new SupplierAlreadyExistException("Supplier already exists!");
            }
            else
            {
                Console.WriteLine("Enter Supplier's Contact");
                string supplierContactInfo = Console.ReadLine();

                Supplier supplier = new Supplier
                {
                    InventoryId = inventoryId,
                    SupplierName = supplierName,
                    SupplierContactInfo = supplierContactInfo
                };

                _supplierManager.Add(supplier);
                Console.WriteLine("Supplier added successfully");
            } 
        }

        public static void UpdateSupplier()
        {

            Console.WriteLine("Enter InventoryId: ");
            int InventoryId = Convert.ToInt32(Console.ReadLine());
            //checking if inventory exists
            if (_supplierManager.IsInventoryExists(InventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exist");
            }
            //checking if supplier exists or not
            Console.WriteLine("Enter Supplier's id: ");
            if (!int.TryParse(Console.ReadLine(), out int suppId))
            {
                throw new InvalidIdException("Invalid ID. Please enter a valid number.");
            }

            var existingSupplier = _supplierManager.GetById(suppId);
            if (existingSupplier != null)
            {
                //checking for duplicate supplier name before updating
                Console.WriteLine("Enter new supplier name: ");
                string newName = Console.ReadLine();
                if (_supplierManager.IsSupplierExists(newName, suppId))
                {
                    Console.WriteLine("A supplier with this name already exists! Please choose a different name.");
                    return;
                }
                //if no duplicates found update suppliers details
                Console.WriteLine("Enter Supplier's Contactinfo:");
                string conInfo = Console.ReadLine();

                existingSupplier.SupplierName = newName;
                existingSupplier.SupplierContactInfo = conInfo;

                _supplierManager.Update(existingSupplier);
                Console.WriteLine("Supplier updated successfully");
            }
            else
            {
                throw new SupplierNotFoundException("No such supplier exists");
            }
        }

        public static void DeleteSupplier()
        {

            Console.WriteLine("Enter InventoryId: ");
            int InventoryId = Convert.ToInt32(Console.ReadLine());
            //checking if inventory exists
            if (_supplierManager.IsInventoryExists(InventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exist");
            }
            //checking if supplier exists or not
            Console.WriteLine("Enter the Supplier id you want to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int suppId))
            {
                throw new InvalidIdException("Invalid ID. Please enter a valid number.");
            }

            var existingSupplier = _supplierManager.GetById(suppId);
            if (existingSupplier != null)
            {
                _supplierManager.Delete(suppId);
                Console.WriteLine("Supplier deleted successfully");
            }
            else

                throw new SupplierNotFoundException("No such supplier exists");

        }

        public static void ViewSupplierDetails()
        {
            Console.WriteLine("Enter InventoryId: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());
            //checking if inventory exists
            if (_supplierManager.IsInventoryExists(inventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exist");
            }

            Console.WriteLine("Enter Supplier name you want the details of: ");
            string SupplierName = Console.ReadLine();
            //checking if supplier exists
            var existingSupplier = _supplierManager.GetByName(SupplierName, inventoryId);
            if (existingSupplier != null)
            {
                Console.WriteLine($"SupplierId: {existingSupplier.SupplierId}\n" +
                    $"SupplierName: {existingSupplier.SupplierName}\n" +
                    $"SupplierContactInfo: {existingSupplier.SupplierContactInfo}\n" +
                    $"==========================================================");
            }
            else Console.WriteLine("No such supplier found with the name " + SupplierName);
        }

        public static void GetAllSuppliers()
        {
            Console.WriteLine("Enter Inventory Id: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());


            if (_supplierManager.IsInventoryExists(inventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exists");
            }
            var suppliers = _supplierManager.GetAll(inventoryId);
            //if inventory is empty 

            if (suppliers == null || !suppliers.Any())
            {
                throw new InventoryIsEmptyException("Inventory is Empty!");
            }
            else
            {
                foreach (var supplier in suppliers)
                {
                    Console.WriteLine(supplier);
                }
            }
            
        }




    }
}
