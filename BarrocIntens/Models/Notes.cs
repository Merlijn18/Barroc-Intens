using System;

namespace BarrocIntens.Models
{
    public class Notes
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }   // optional: timestamp
    }
}
