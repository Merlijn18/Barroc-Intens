using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Models
{
    internal class AgendaKlus
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }
        public string Type { get; set; } 
        public string Titel { get; set; }   
        public string ExtraInfo { get; set; }
    }
}
