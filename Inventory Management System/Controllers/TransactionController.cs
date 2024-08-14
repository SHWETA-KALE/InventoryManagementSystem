using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Inventory_Management_System.Data;
using Inventory_Management_System.Exceptions;
using Inventory_Management_System.Migrations;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repository;
using Microsoft.EntityFrameworkCore;
using TransactionManager = Inventory_Management_System.Models.TransactionManager;

namespace Inventory_Management_System.Controllers
{
    internal class TransactionController
    {
        private readonly static ProductManager _productManager = new ProductManager(new InventoryContext());
        private readonly static TransactionManager transactionManager = new TransactionManager(new InventoryContext());

        public static void DisplayTransactionMenu()
        {


            Console.WriteLine("============================TRANSACTION MANAGEMENT===========================");
            while (true)
            {
                Console.WriteLine("1. Add Stock\n" +
                    "2. Remove Stock\n" +
                    "3. View Transaction History Of A Product\n" +
                    "4. Go Back To Main Menu\n\n" +
                    "Enter your choice: ");
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
                    Console.WriteLine(".......................................................................................................");
                    DoTransactionTask(choice);
                }
                //catch (FormatException)
                //{
                //    Console.WriteLine("Please enter numbers only!\n");
                //}
                //catch (InvalidIdException iie)
                //{
                //    Console.WriteLine("Invalid Id!, Please enter a valid id");
                //}
                //catch(InvalidQuantityException iqe)
                //{
                //    Console.WriteLine(iqe.Message);
                //}
                //catch (ProductNotFoundException pnf)
                //{
                //    Console.WriteLine(pnf.Message);
                //}
                catch (Exception ex ){
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
        static void DoTransactionTask(int choice)
        {
            switch (choice)
            {
                case 1:
                    AddStock();
                    break;
                case 2:
                    RemoveStock();
                    break;
                case 3:
                    ViewTransactionHistory();
                    break;
                case 4:
                    MainMenu.DisplayMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.\n");
                    break;
            }
        }
        public static void AddStock()
        {
            //entering inventory id => to understand in which inventory we are adding the stocks
            Console.WriteLine("Enter Inventory Id: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            //checking if that inventory exists 
            if (transactionManager.IsInventoryExists(inventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exists");
            }
   
            Console.WriteLine("Enter id: ");
            int prodid = Convert.ToInt32(Console.ReadLine());
            if (prodid <= 0)
            {
                throw new InvalidIdException("Invalid ID. Please enter a valid number.\n");
            }

            var existingProduct = _productManager.GetById(prodid, inventoryId);
            if (existingProduct != null)
            {

                Console.WriteLine("Enter quantity: ");
                int quant = Convert.ToInt32(Console.ReadLine());
                if (quant <= 0)
                {
                    throw new InvalidQuantityException("Quantity must be greater than zero.");
                }
                transactionManager.Add(inventoryId,prodid, quant);
                Console.WriteLine("Stock added successfully");
            }
            else
            {
                throw new ProductNotFoundException("Product not found!\n");
            }
        }
        public static void RemoveStock()
        {

            Console.WriteLine("Enter Inventory Id: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            //checking if that inventory exists 
            if (transactionManager.IsInventoryExists(inventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exists");
            }


            Console.WriteLine("Enter id: ");
            int prodid = Convert.ToInt32(Console.ReadLine());
            if (prodid <= 0)
            {
                throw new InvalidIdException("Invalid ID. Please enter a valid number.\n");
            }
            var existinPproduct = _productManager.GetById(prodid, inventoryId);
            if (existinPproduct != null)
            {
                Console.WriteLine("Enter quantity: ");
                int quant = Convert.ToInt32(Console.ReadLine());
                if (quant <= 0)
                {
                    throw new InvalidQuantityException("Quantity must be greater than zero.");
                }
                transactionManager.Remove(prodid, quant);
                Console.WriteLine("Stock removed successfully");
            }else 
                throw new ProductNotFoundException("Product not found!\n");
        }
        public static void ViewTransactionHistory()
        {
            Console.WriteLine("Enter Inventory Id: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            //checking if that inventory exists 
            if (transactionManager.IsInventoryExists(inventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exists");
            }

            Console.WriteLine("enter id: ");
            int prodid = Convert.ToInt32(Console.ReadLine());

            if (prodid <= 0)
            {
                throw new InvalidIdException("Invalid ID. Please enter a valid number.\n");
            }
            var existingProduct = _productManager.GetById(prodid, inventoryId);
            if (existingProduct != null)
            {
                var transactions = transactionManager.ViewTransactionHistory(prodid);
                transactions.ForEach(x => Console.WriteLine(x));
            }else
                throw new ProductNotFoundException("Product not found!\n");

        }


    }
}

