using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory_Management_System.Data;

namespace Inventory_Management_System.Models
{
    internal class TransactionManager
    {
        //accessing table
        private readonly InventoryContext _context;

        public TransactionManager(InventoryContext inventoryContext)
        {
            _context = inventoryContext;
        }
        public List<Transaction> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }

        public bool IsInventoryExists(int id) {
            var existingInventory = _context.Inventories.Where(x => x.InventoryId == id).FirstOrDefault();
            return existingInventory == null;
        }
        public void Add(int inventoyIdint, int prodid, int quantity)
        {
            var product = _context.Products.Find(prodid);
            if (product != null) { 
                product.Quantity += quantity;
            }

            //transaction record
            var transaction = new Transaction
            {
                ProductId = prodid,
                Type = "Add Stock",
                Quantity = quantity,
                TransactionDate = DateTime.Now

            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void Remove(int prodid, int quantity) {
            var product = _context.Products.Find(prodid);
            if (product != null)
            {
                product.Quantity -= quantity;

                //transaction record
                var transaction = new Transaction
                {
                    ProductId = prodid,
                    Type = "Remove Stock",
                    Quantity = quantity,
                    TransactionDate = DateTime.Now

                };
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("");
            }
        }

        public List<Transaction> ViewTransactionHistory(int prodid)
        {
            var transactionHistory = _context.Transactions.Where(x=>x.ProductId == prodid).OrderBy(t=>t.TransactionDate).ToList();
            if (transactionHistory.Count == 0) {
                Console.WriteLine("No transaction found");
            }
            return transactionHistory;

        }



    }
}
