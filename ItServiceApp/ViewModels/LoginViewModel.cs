using System.ComponentModel.DataAnnotations;

namespace ItServiceApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 chracter")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
