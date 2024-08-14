using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Exceptions
{
    internal class InvalidQuantityException:Exception
    {
        public InvalidQuantityException(string message):base(message) { }
        
    }
}
