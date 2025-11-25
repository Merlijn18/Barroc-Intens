using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Models
{
    internal class Product
    {
        public int Id { get; set; }
        public string Productname { get; set; }
        public  float Price { get; set; }
        public int Stock { get; set; }
        public int OrderQuantity { get; set; }

    }
}
