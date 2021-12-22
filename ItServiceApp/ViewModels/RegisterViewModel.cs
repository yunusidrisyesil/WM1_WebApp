using System.ComponentModel.DataAnnotations;

namespace ItServiceApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [Display(Name="Name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        [Display(Name = "Surname")]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100,MinimumLength = 6,ErrorMessage ="Password must be at least 6 chracter")]
        public string Password{ get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 chracter")]
        [Compare(nameof(Password),ErrorMessage ="Passwords are not same")]
        public string ConfirmPassword { get; set; }

    }
}
