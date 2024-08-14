using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    internal class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        public string Name { get; set; }

        //nav prop
        public List<Product> Products { get; set; } = new List<Product>();

        public List<Supplier> Suppliers { get; set; } = new List<Supplier>();

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public override string ToString()
        {
            return $"InventoryId: {InventoryId}\n" +
                $"InventoryName: {Name}\n";
        }
    }
}
