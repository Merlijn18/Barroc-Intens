using BarrocIntens.Data;
using BarrocIntens.Models;
using Microsoft.EntityFrameworkCore;
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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Text;

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
        private async void ApproveAndGenerateInvoice_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                var offer = await context.Offers
                    .Include(o => o.Items)
                    .Include(o => o.Customer)
                    .FirstOrDefaultAsync(o => o.Id == SelectedOffer.Id);

                if (offer == null) return;

                // Markeer als goedgekeurd
                offer.Status = OfferStatus.Goedgekeurd;

                // Maak factuur aan
                var factuur = new Factuur
                {
                    klant_Id = offer.CustomerId,
                    offerte_id = offer.Id,
                    datum = DateTime.Now,
                    bedrag = offer.Total,
                    btw = 21,
                    valuta = "EUR",
                    wisselkoers = 1,
                    wisselkoersdatum = DateTime.Now,
                    status = "Open",
                    factuurnummer = await GenerateUniqueInvoiceNumber(context)
                };

                context.Factuurs.Add(factuur);
                await context.SaveChangesAsync();
            }


        }
        private async Task<int> GenerateUniqueInvoiceNumber(AppDbContext context)
        {
            var last = await context.Factuurs
                .OrderByDescending(f => f.factuurnummer)
                .FirstOrDefaultAsync();

            return (last?.factuurnummer ?? 2025000) + 1;
        }
    }
}
