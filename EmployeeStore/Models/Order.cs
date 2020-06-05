using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [BindNever]
        public bool Shipped { get; set; }

        [Required(ErrorMessage = "Please, enter your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, enter your Address")]
        public string Line1 { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, enter your City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please, enter your Phone number")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please, enter your Post index")]
        public string Zip { get; set; }

        public DateTime orderTime = DateTime.Now;

        public DateTime OrderTime
        {
            get { return orderTime; }
            set { orderTime = value; }

        }

        public decimal TotalAmount { get; set; }
    }
}
