using BarrocIntens.Data;
using BarrocIntens.Models;
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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.Pages.Sales
{

    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double d)
                return $"€{d:N2}";
            if (value is decimal dec)
                return $"€{dec:N2}";
            return value?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
    public sealed partial class SalesOverViewPage : Page
    {
        private ObservableCollection<Offer> Offers = new ObservableCollection<Offer>();
        private Offer SelectedOffer;

        public SalesOverViewPage()
        {
            this.InitializeComponent();

            // Zet de ObservableCollection als ItemsSource voor de ListView
            OfferListView.ItemsSource = Offers;

            // Laad data uit database
            LoadOffersFromDatabase();
        }

        private async void LoadOffersFromDatabase()
        {
            using (var db = new AppDbContext())
            {
                // Include Customer en Items zodat alles geladen wordt
                var offersFromDb = await db.Offers
                    .Include(o => o.Customer)
                    .Include(o => o.Items)
                    .ToListAsync();

                Offers.Clear();
                foreach (var offer in offersFromDb)
                {
                    Offers.Add(offer);
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Inlog.InlogOverViewPage));
        }
        private async void CreateOffer_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                // 1. Nieuwe klant aanmaken met verplichte velden
                var newCustomer = new Customer
                {
                    Name = "Nieuwe klant",
                    Street = "Onbekend",
                    PostalCode = "0000",
                    City = "Onbekend"
                };

                db.Customers.Add(newCustomer);
                await db.SaveChangesAsync();

                // 2. Nieuwe offerte aanmaken met verplichte velden
                var newOffer = new Offer
                {
                    OfferNumber = GenerateOfferNumber(),
                    ContractNumber = GenerateContractNumber(),
                    CustomerNumber = GenerateCustomerNumber(),

                    Status = OfferStatus.Concept,
                    Date = DateTime.Now,
                    ValidUntil = DateTime.Now.AddDays(30),

                    CustomerId = newCustomer.Id,
                    Customer = newCustomer, // ✔️ required navigatie

                    PaymentTerms = "Betaling binnen 30 dagen na factuurdatum.",
                    DeliveryTerms = "Levering binnen 7 werkdagen na akkoord.",
                    ExtraConditions = "Prijzen exclusief btw.",

                    ContactPerson = "Onbekend",
                    SignatureName = "Barroc Intens BV",

                    Items = new List<OfferItem>()
                };
                db.Offers.Add(newOffer);
                await db.SaveChangesAsync();

                // 3. Voeg toe aan ObservableCollection zodat het direct in de UI verschijnt
                Offers.Add(newOffer);

                // 4. Navigeer direct naar detailpagina voor bewerken
                Frame.Navigate(typeof(OfferDetailsPage), newOffer);
            }
        }

        private string GenerateOfferNumber() => $"OFF-{DateTime.Now:yyyyMMddHHmmss}";
        private string GenerateContractNumber() => $"CN-{DateTime.Now:yyyyMMddHHmmss}";
        private string GenerateCustomerNumber() => $"CUST-{DateTime.Now:yyyyMMddHHmmss}";

        private void EditOffer_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Offer offer)
            {
                // Navigeer naar de edit page en geef de aangeklikte offerte door
                Frame.Navigate(typeof(OfferEditPage), offer.Id);
            }
        }


        private async void FilterConcept_Click(object sender, RoutedEventArgs e)
        {
            await LoadOffersByStatus(OfferStatus.Concept);
        }

        private async void FilterVerstuurd_Click(object sender, RoutedEventArgs e)
        {
            await LoadOffersByStatus(OfferStatus.Verstuurd);
        }
        private async void FilterAll_Click(object sender, RoutedEventArgs e)
        {
            await LoadAllOffers();
        }
        private async Task LoadAllOffers()
        {
            using (var db = new AppDbContext())
            {
                var allOffers = await db.Offers
                    .Include(o => o.Customer)
                    .Include(o => o.Items)
                    .ToListAsync();

                Offers.Clear();
                foreach (var offer in allOffers)
                    Offers.Add(offer);
            }

            OfferListView.ItemsSource = Offers;
        }
        private async Task LoadOffersByStatus(OfferStatus status)
        {
            using (var db = new AppDbContext())
            {
                var filteredOffers = await db.Offers
                    .Include(o => o.Customer)
                    .Include(o => o.Items)
                    .Where(o => o.Status == status)
                    .ToListAsync();

                Offers.Clear();
                foreach (var offer in filteredOffers)
                {
                    Offers.Add(offer);
                }
            }
        }
        private void OfferListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Offer selectedOffer)
            {
                Frame.Navigate(typeof(OfferDetailsPage), selectedOffer);
            }
        }

    }

}