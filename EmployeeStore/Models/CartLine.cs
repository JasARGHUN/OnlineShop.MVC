using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Models
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public int Id { get; set; }
        public Employee Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
    }
}
