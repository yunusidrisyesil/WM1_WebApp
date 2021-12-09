using System;
using System.Collections.Generic;

#nullable disable

namespace FirstMVCProject.Models
{
    public partial class OrderDetail1
    {
        public string CustomerName { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public float Discount { get; set; }
        public string Shipper { get; set; }
        public DateTime? OrderDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Supplier { get; set; }
        public string FirstName { get; set; }
    }
}
