using System.ComponentModel.DataAnnotations;

namespace ItServiceApp.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 chracter")]
        public string OldPassword { get; set; }

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
    }
}
