using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Models
{
    // Offer.cs

    public enum OfferStatus
    {
        Concept = 0,
        Verstuurd = 1
    }
    public class Offer
    {
        public int Id { get; set; }
        public string OfferNumber { get; set; }
        public string PaymentTerms { get; set; }          // Betalingsvoorwaarden
        public string DeliveryTerms { get; set; }         // Leveringsvoorwaarden
        public DateTime? ValidUntil { get; set; }         // Geldigheidsduur
        public string ExtraConditions { get; set; }       // Extra afspraken
        public string ContactPerson { get; set; }         // Contactpersoon
        public string SignatureName { get; set; }         // Naam handtekening of akkoord

        public OfferStatus Status { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string CustomerNumber { get; set; }
        public string ContractNumber { get; set; }
        public List<OfferItem> Items { get; set; } = new List<OfferItem>();

        public double Total => Items.Sum(i => i.Subtotal);
        public double VAT => Total * 0.21;
        public double TotalwithVAT => Total + VAT;


    }
}
