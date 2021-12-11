using System.ComponentModel.DataAnnotations;

namespace FirstMVCProject.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee name cannot be empty")]
        [StringLength(10, ErrorMessage = "Employee name cannot exceed 10 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Employee lastname cannot be empty")]
        [StringLength(20,ErrorMessage = "Employee lastname cannot exceed 20 charaters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
