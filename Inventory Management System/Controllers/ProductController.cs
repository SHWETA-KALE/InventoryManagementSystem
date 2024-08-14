using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory_Management_System.Data;
using Inventory_Management_System.Exceptions;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;

namespace Inventory_Management_System.Controllers
{
    internal class ProductController
    {
        private readonly static ProductManager _productManager = new ProductManager(new InventoryContext());
        public static void DisplayProdMenu()
        {
            Console.WriteLine("==============================PRODUCT MANAGEMENT===================================");
            while (true)
            {

                Console.WriteLine($"1.Add Product\n" +
                    $"2.Update Product\n" +
                    $"3.Delete Product\n" +
                    $"4.View Product Details\n" +
                    $"5.View All Products\n" +
                    $"6.Exit\n" +
                    $"7.Go back to main menu\n");
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
                    DoTask(choice);
                }
                //catch (FormatException)
                //{
                //    Console.WriteLine("Please enter numbers only!\n");

                //}
                //catch (InvalidIdException iie)
                //{
                //    Console.WriteLine("Invalid Id!, Please enter a valid id");
                //}
                //catch (ProductAlreadyExistsException pae)
                //{
                //    Console.WriteLine(pae.Message);
                //}
                //catch (ProductNotFoundException pf)
                //{
                //    Console.WriteLine(pf.Message);
                //}
                //catch (InvalidQuantityException iq)
                //{
                //    Console.WriteLine(iq.Message);
                //}
                //catch (InvalidPriceException ip)
                //{
                //    Console.WriteLine(ip.Message);
                //}
                //catch (ProductUpdateException pu)
                //{
                //    Console.WriteLine(pu.Message);
                //}
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        static void DoTask(int choice)
        {

            switch (choice)
            {
                case 1:
                    AddProduct();
                    break;
                case 2:
                    UpdateProduct();
                    break;
                case 3:
                    Deleteproduct();
                    break;
                case 4:
                    ViewProductDetails();
                    break;
                case 5:
                    GetAllProducts();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                case 7:
                    MainMenu.DisplayMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.\n\n");
                    break;
            }
        }

        public static void AddProduct()
        {
            //first check product exists or not
            //checking by name:

            Console.WriteLine("Enter Inventory Id: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());

            if (_productManager.IsInventoryExists(inventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exists");
            }

            Console.WriteLine("Enter Product name you want to add: ");

            string productName = Console.ReadLine();

            Product existingProduct = _productManager.GetByName(productName, inventoryId);
            if (existingProduct != null)
            {
                throw new ProductAlreadyExistsException("Product already exists");
            }
            else
            {
                Console.WriteLine("Enter Description of the product: ");
                string prodDesc = Console.ReadLine();

                Console.WriteLine("Enter the Quantity you want to add: ");
                if (!int.TryParse(Console.ReadLine(), out int prodQuantity) || prodQuantity <= 0)
                {
                    throw new InvalidQuantityException("Quantity must be greater than zero.");
                }

                Console.WriteLine("Enter Price of the product");
                if (!double.TryParse(Console.ReadLine(), out double prodPrice) || prodPrice <= 0)
                {
                    throw new InvalidPriceException("Price must be greater than zero.");

                }
                //double prodPrice = Convert.ToDouble(Console.ReadLine());
                //if (prodPrice <= 0)
                //{
                //    throw new InvalidPriceException("Price must be greater than zero.");
                //}

                var product = new Product
                {
                    InventoryId = inventoryId,
                    ProductName = productName,
                    Description = prodDesc,
                    Quantity = prodQuantity,
                    Price = prodPrice
                };
                Console.WriteLine();

                _productManager.Add(product);
                Console.WriteLine("New Product added successfully.\n");
            }

        }

        public static void UpdateProduct()
        {
            Console.WriteLine("Enter Inventory Id: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());

            if (_productManager.IsInventoryExists(inventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exists");
            }

            //first check product exists or not
            Console.WriteLine("Enter Product Id you want to update: ");
            if (!int.TryParse(Console.ReadLine(), out int prodId))
            {
                throw new InvalidIdException("Invalid ID. Please enter a valid number.");
            }

            //checking if prod exists 
            var existingproduct = _productManager.GetById(prodId,inventoryId);
            if (existingproduct != null)
            {
                //checking for duplicate prod name before updating
                Console.WriteLine("Enter new Product Name: ");
                string newProductName = Console.ReadLine();
                if (_productManager.IsProductNameExists(newProductName, prodId))
                {
                    Console.WriteLine("A product with this name already exists! Please choose a different name.");
                    return;
                }
                // If no duplicate, update the product details
                existingproduct.ProductName = newProductName;

                Console.WriteLine("Enter new Product Description: ");
                existingproduct.Description = Console.ReadLine();

                Console.WriteLine("Enter new Product Price: ");
                if (!double.TryParse(Console.ReadLine(), out double prodPrice) || prodPrice <= 0)
                {

                    // double newProductPrice = Convert.ToDouble(Console.ReadLine());{
                    throw new InvalidPriceException("Price must be greater than zero.");

                }

                //if (newProductPrice <= 0)
                //{
                //    throw new InvalidPriceException("Price must be greater than zero.");
                //}
                // existingproduct.Price = newProductPrice;
                existingproduct.Price = prodPrice;

                _productManager.Update(existingproduct);
                Console.WriteLine("Product Updated successfully");
            }
            else
                throw new ProductNotFoundException("No such product exists");

        }

        public static void Deleteproduct()
        {
            Console.WriteLine("Enter Inventory Id: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());

            if (_productManager.IsInventoryExists(inventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exists");
            }

            Console.WriteLine("Enter the Product id: ");
            if (!int.TryParse(Console.ReadLine(), out int prodId))
            {
                throw new InvalidIdException("Invalid ID. Please enter a valid number.");

            }

            //checking if prod exists 
            var existingproduct = _productManager.GetById(prodId, inventoryId);
            if (existingproduct != null)
            {
                _productManager.Delete(prodId);
                Console.WriteLine("Product Deleted Successfully");
            }
            else
                throw new ProductNotFoundException("Product does not exist");

        }

        public static void ViewProductDetails()
        {
            Console.WriteLine("Enter Inventory Id: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());

            if (_productManager.IsInventoryExists(inventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exists");
            }

            Console.WriteLine("Enter Product name you want the details of: ");
            string productName = Console.ReadLine();
            Product product = _productManager.GetByName(productName, inventoryId);
            if (product != null)
            
                Console.WriteLine(product);
            else
                Console.WriteLine("No product found with the name  " + productName);
            

        }

        public static void GetAllProducts()
        {
            Console.WriteLine("Enter Inventory Id: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());


            if (_productManager.IsInventoryExists(inventoryId))
            {
                throw new InventoryDoesNotExistException("Inventory does not exists");
            }
            var products = _productManager.GetAll(inventoryId);
            //if inventory is empty 
           
            if (products == null || !products.Any())
            {
                throw new InventoryIsEmptyException("Inventory is Empty!");
            }
            else
            {
                foreach (var product in products)
                {
                    Console.WriteLine(product);
                }
            }

        }

    }
}
