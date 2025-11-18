using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Models
{
    public class OfferItem
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
        public string ProductName { get; set; } // Omschrijving
        public string ProductNumber { get; set; }
        public int Quantity { get; set; } // Aantal
        public double UnitPrice { get; set; } // Prijs per stuk
        public double Subtotal => Quantity * UnitPrice; // Totaalprijs
    }
}
