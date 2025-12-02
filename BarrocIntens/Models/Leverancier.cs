using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Models
{
    internal class Leverancier
    {
        public int Id { get; set; }
        public string Leveranciernaam { get; set; }
        public string Contactpersoon { get; set; }
        public int Telefoonnummer { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }

        public Boolean Kwaliteit { get; set; }
    }
}
