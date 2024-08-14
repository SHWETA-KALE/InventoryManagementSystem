using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Inventory_Management_System.Models
{
    internal class Transaction
    {
        [Key]
        public int TransId { get; set; }

        public string Type { get; set; }
        public Product Product { get; set; } //nav property

        [ForeignKey("Product")] 
        public int ProductId { get; set; } //fk

        public int Quantity { get; set; }

        public DateTime TransactionDate { get; set; }

        public Inventory Inventory { get; set; } //nav property

        [ForeignKey("Inventory")]

        public int? InventoryId { get; set; }


        public override string ToString()
        {
            return  $"InventoryId: {InventoryId}" +
                $"TransactionId: {TransId}.\n" +
                $"Quantity: {Quantity}.\n" +
                $"ProductId: {ProductId}.\n" +
                $"TransactionDate: {TransactionDate}.\n" +
                $"Transaction Type: {Type}.\n" +
                $"==============================================================\n";
        }
    }
}
