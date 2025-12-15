using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Models
{
    internal class Factuur
    {
        public int Id { get; set; }
        public int klant_Id { get; set; }
        public int offerte_id { get; set; }
        public DateTime datum {  get; set; }
        public double bedrag { get; set; }
        public double btw { get; set; }
        public string valuta { get; set; }
        public DateTime wisselkoersdatum { get; set; }
        public string status { get; set; }
        public int factuurnummer {  get; set; }
    }
}
