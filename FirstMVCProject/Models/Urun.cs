using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstMVCProject.Models
{
    public class Urun
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

    }

    public static class ProductManager
    {
        public static List<Urun> GetProducts()
        {
            var list = new List<Urun>();
            list.Add(new Urun() { Name = "Book", Price = 15 });
            list.Add(new Urun() { Name = "Pen", Price = 5 });
            list.Add(new Urun() { Name = "Notepad", Price = 7 });
            return list;
        }
    }
}
