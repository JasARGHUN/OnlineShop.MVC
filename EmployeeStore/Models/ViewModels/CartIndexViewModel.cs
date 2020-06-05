using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Models.ViewModels
{
    public class CartIndexViewModel
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
