using System.Threading.Channels;
using Inventory_Management_System.Controllers;
using Inventory_Management_System.Data;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repository;

namespace Inventory_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ProductManager productManager = new ProductManager(new InventoryContext());

            //add

            //Product product = new Product
            //{
            //    ProductName = "M.A.C Studio Fix",
            //    Description = "Makeup",
            //    Quantity = 6,
            //    Price = 2500

            //};
            //Console.WriteLine(productManager.Add(product));



            //Update 
            //Product product = new Product
            //{
            //    ProductName = "Dyson",
            //    Description = "Update Demo",
            //    Quantity = 10,
            //    Price = 300000
            //};

            //productManager.Update(product);

            //delete
            //productManager.Delete(5);

            //SupplierManager supplierManager = new SupplierManager(new InventoryContext());

            //Supplier supplier = new Supplier
            //{
            //    //SupplierId = 2,
            //    SupplierName = "Test",
            //    SupplierContactInfo = "7654345678"
            //};
            ////add 
            // supplierManager.Add(supplier);

            //update => give id in obj initialiser
            // supplierManager.Update(supplier);

            //delete
            //supplierManager.Delete(2);

            //var suppliers = supplierManager.GetAll();
            //suppliers.ForEach(emp => Console.WriteLine(emp));

            //var products = productManager.GetAll();
            //products.ForEach(emp => Console.WriteLine(emp));


            //==================================================
            // var context = new InventoryContext();
            //var productManager = new ProductManager(context);
            //ProductController.AddProduct(productManager);
            //ProductController.UpdateProduct(productManager);
            //ProductController.Deleteproduct(productManager);
            // ProductController.ViewProductDetails(productManager);
            //ProductController.GetAllProducts(productManager);

            //var supplierManager = new SupplierManager(context);
            //SupplierController.AddSupplier(supplierManager);
            //SupplierController.UpdateSupplier(supplierManager);
            // SupplierController.DeleteSupplier(supplierManager);
            //SupplierController.ViewSupplierDetails(supplierManager);
            //SupplierController.GetAllSuppliers(supplierManager);

            MainMenu.DisplayMenu();
            
            

        }
    }
}








//commands for migration

//add-migration string(ProductTable ver1)
//update-database