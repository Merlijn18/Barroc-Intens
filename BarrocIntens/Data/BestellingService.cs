using System;
using BarrocIntens.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BarrocIntens.Data
{
    class BestellingService
    {
        public static event Action<Order> BestellingToegevoegd;
        public static void VoegBestellingToe(Order bestelling)
        {
            using var db = new AppDbContext();
            db.Bestellingen.Add(bestelling);
            db.SaveChanges();

            BestellingToegevoegd?.Invoke(bestelling);
        }
    }
}
