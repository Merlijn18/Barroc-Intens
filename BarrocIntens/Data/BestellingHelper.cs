using BarrocIntens.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    internal class BestellingHelper
    {
        private static readonly List<Order> _notifications = new();

        // Event dat wordt getriggerd wanneer er een nieuwe bestelling toegevoegd wordt
        public static event Action<Order> BestellingToegevoegd;

        public static void AddNotification(Order order)
        {
            _notifications.Add(order);
            BestellingToegevoegd?.Invoke(order);
        }

        public static List<Order> GetNotifications()
        {
            return _notifications;
        }

        public static void RemoveNotification(Order order)
        {
            _notifications.Remove(order);
        }

        public static void Clear()
        {
            _notifications.Clear();
        }
    }
}
    

