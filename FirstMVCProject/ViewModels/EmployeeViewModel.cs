using System;
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
        [StringLength(20,ErrorMessage = "Employee lastname cannot exceed 20 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Employee birthdate cannot be empty")]
        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage ="Employee title cannot be empty")]
        [StringLength(30,ErrorMessage = "Employee title cannot exceed 30 characters")]
        public string Title { get; set; }

        [StringLength(60,ErrorMessage = "Employee address cannot exceed 60 characters")]
        public string Address { get; set; }

        [StringLength(15,ErrorMessage = "City name cannot exceed 15 characters")]
        public string City { get; set; }

        [StringLength(15,ErrorMessage = "Country name cannot exceed 15 characters")]
        public string Country { get; set; }
        
        [StringLength(24,ErrorMessage = "Home Phone cannot exceed 24 characters")]
        public string HomePhone { get; set; }

        [StringLength(25,ErrorMessage = "Title Of Courtesy cannot exceed 25 characters")]
        public string TitleOfCourtesy { get; set; }

        public DateTime HireDate { get; set; }
    }
}
