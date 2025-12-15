using BarrocIntens.Data;
using BarrocIntens.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.EntityFrameworkCore;

namespace BarrocIntens.Pages.Sales
{
    public sealed partial class OfferDetailsPage : Page
    {
        private Offer SelectedOffer;
        private AppDbContext db = new AppDbContext();

        public OfferDetailsPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Offer offer)
            {
                using (var db = new AppDbContext()) // Maak een context aan
                {
                    SelectedOffer = await db.Offers
                        .Include(o => o.Customer)
                        .Include(o => o.Items)
                        .FirstOrDefaultAsync(o => o.Id == offer.Id);
                }

                this.DataContext = SelectedOffer;
            }
        }
        private async void SaveOffer_Click(object sender, RoutedEventArgs e)
        {
            // Wijzigingen zijn al gebonden via x:Bind
            await db.SaveChangesAsync();

            // Terug naar overzicht
            if (Frame.CanGoBack) Frame.GoBack();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new OfferItem
            {
                ProductName = "Nieuw product",
                ProductNumber = "000",
                Quantity = 1,
                UnitPrice = 0,
                OfferId = SelectedOffer.Id
            };

            SelectedOffer.Items.Add(newItem);
            db.OfferItems.Add(newItem);
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is OfferItem item)
            {
                SelectedOffer.Items.Remove(item);
                db.OfferItems.Remove(item);
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
