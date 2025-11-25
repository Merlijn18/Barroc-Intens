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
       
        public int OrderId { get; set; }
        public string Productname { get ; set; }
        public string Suppliername { get ; set; }
        public int OrderQuantity { get; set; }


        public DateTime OrderDate { get; set; }


        public DateTime? ExpectedDeliveryDate { get; set; }


        public decimal UnitPrice { get; set; }


        public decimal TotalPrice => OrderQuantity * UnitPrice;


        public string Status { get; set; }


        public string Remark { get; set; }

    }
}
