using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Exceptions
{
    internal class InvalidIdException:Exception
    {
        public InvalidIdException(string message):base(message) { }
    }
}
