using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstMVCProject.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName{ get; set; }
        public string CompanyName { get; set; }
        [Range(0,99999999999,ErrorMessage = "Product unit price has to be between 0-99999999999")]
        public decimal? UnitPrice { get; set; }

    }
}
