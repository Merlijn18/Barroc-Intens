using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArticleNumber { get; set; }
        public double LeasePrice { get; set; }
        public double InstallationCost { get; set; }
    }
}
