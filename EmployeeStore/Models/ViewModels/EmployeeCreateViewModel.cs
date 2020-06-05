using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeStore.Models.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required(ErrorMessage = "Enter employee name")]
        public string Name { get; set; }

        //[Required]
        //[RegularExpression(@"^[a-zA-Z0-9_.+]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        //    ErrorMessage = "Invalid email format")]
        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }

        public decimal Price { get; set; }

        [Required(ErrorMessage = "Enter employee description")]
        public string Description { get; set; }
        public string Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}
