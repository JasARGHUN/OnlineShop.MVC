using System.ComponentModel.DataAnnotations;


namespace EmployeeStore.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Role Name is required")]
        [RegularExpression(@"[a-z]{3,50}$",
         ErrorMessage = "Only lowercase letters allowed.")]
        public string RoleName { get; set; }
    }
}
