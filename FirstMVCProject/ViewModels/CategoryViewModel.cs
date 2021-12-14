using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstMVCProject.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Kategori adı gereklidir.")]
        [StringLength(15,ErrorMessage ="Kategori adı 15 karakterden büyük olamaz")]
        [Display(Name = "Kategori Adı")]
        public string CategoryName { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        public int ProductCount { get; set; }
    }
}
