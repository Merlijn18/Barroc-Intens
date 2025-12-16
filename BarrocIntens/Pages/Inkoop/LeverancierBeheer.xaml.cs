using BarrocIntens.Data;
using BarrocIntens.Models;
using BarrocIntens.Pages.Inkoop;
using BarrocIntens.Pages.Inlog;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace BarrocIntens.Pages.Inkoop
{
    public sealed partial class LeverancierBeheer : Page
    {
        public LeverancierBeheer()
        {
            InitializeComponent();

            LoadLeveranciers();
            BestellingService.BestellingToegevoegd += ToonMelding;
            LoadNotifications();

            BestellingHelper.BestellingToegevoegd += OnNieuweBestelling;

            var notifications = BestellingHelper.GetNotifications();
            System.Diagnostics.Debug.WriteLine($"Aantal meldingen: {notifications.Count}");

            NotificationListView.ItemsSource = notifications;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            User.LoggedInUser = null;
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void ToonMelding(Bestelling bestelling)
        {
            // Je kunt hier optioneel een pop-up of toast notificatie tonen
        }

        private void LoadLeveranciers()
        {
            using var db = new AppDbContext();
            var leveranciers = db.Leveranciers.ToList();
            LeverancierListView.ItemsSource = leveranciers;
        }

        private void LeverancierAanmaken_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LeverancierAanmaken));
        }

        private void LoadNotifications()
        {
            using var db = new AppDbContext();

            var approvedOrders = db.Bestellingen
                                   .Where(b => b.Status == "Goedgekeurd")
                                   .ToList();

            foreach (var order in approvedOrders)
            {
                if (!BestellingHelper.GetNotifications().Any(o => o.Id == order.Id))
                    BestellingHelper.AddNotification(order);
            }

            NotificationListView.ItemsSource = BestellingHelper.GetNotifications();
        }

        private void OnNieuweBestelling(Bestelling order)
        {
            _ = DispatcherQueue.TryEnqueue(() =>
            {
                var notifications = BestellingHelper.GetNotifications();
                NotificationListView.ItemsSource = null;
                NotificationListView.ItemsSource = notifications;
            });
        }
    }
}
