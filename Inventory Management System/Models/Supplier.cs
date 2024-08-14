using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    internal class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
       
        public string SupplierContactInfo {  get; set; }

        public Inventory Inventory { get; set; } //nav property

        [ForeignKey("Inventory")]

        public int? InventoryId { get; set; }


        public override string ToString()
        {
            return $"InventoryId: {InventoryId}\n" +
                $"SupplierId: {SupplierId}\n" +
                $"SupplierName: {SupplierName}\n" +
                $"SupplierContactInfo: {SupplierContactInfo}\n" +
                $"==================================================\n";
        }
    }
}
