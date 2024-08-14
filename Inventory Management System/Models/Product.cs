using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    internal class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }

        public Inventory Inventory { get; set; } //nav property

        [ForeignKey("Inventory")]

        public int? InventoryId { get; set; }

        public override string ToString()
        {
            return $"InventoryId: {InventoryId}\n" +
                $"ProductId: {ProductId}\n" +
                $"ProductName: {ProductName}\n" +
                $"Description: {Description}\n" +
                $"Quantity: {Quantity}\n" +
                $"Price: {Price}\n" +
                $"===========================================\n";

        }

    }
}
