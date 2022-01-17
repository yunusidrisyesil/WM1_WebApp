using System.ComponentModel.DataAnnotations;

namespace ItServiceApp.ViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 chracter")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 chracter")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords are not same")]
        public string ConfirmPassword { get; set; }

        public string UserID { get; set; }
        public string Code { get; set; }
    }
}
