using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Models
{
    public class CoffeeBean
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArticleNumber { get; set; }
        public string Description { get; set; }
        public double PricePerKg { get; set; }
    }
}
