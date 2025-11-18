using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Models
{
    class Bestelling
    {
        public int Id { get; set; }
        public int Ordernumber{ get; set; }
        public DateTime Date_Of_Order { get; set; }
        public string Customername { get; set; }
        public string Customer_Email { get; set; }
        public int Customer_Phonenumber { get; set; }
        public Boolean Order_Status { get; set; }

    }
}
