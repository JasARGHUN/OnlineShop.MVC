using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Models.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        [RegularExpression(@"[a-z]{3,50}$",
         ErrorMessage = "Only lowercase letters allowed.")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
