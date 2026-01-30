using BarrocIntens.Data;
using BarrocIntens.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BarrocIntens.Pages.Sales
{
    public sealed partial class OfferCreatePage : Page
    {
        private readonly AppDbContext _context = new();
        public Offer NewOffer { get; set; }
        public ObservableCollection<OfferItem> Items { get; set; } = new();

        public ObservableCollection<Customer> CustomerResults { get; set; } = new();

        public OfferCreatePage()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        // ===============================
        // KLANT ZOEKEN
        // ===============================
        private void CustomerSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = CustomerSearchBox.Text.ToLower();
            if (string.IsNullOrWhiteSpace(query))
            {
                CustomerListView.Visibility = Visibility.Collapsed;
                CustomerResults.Clear();
                return;
            }

            var results = _context.Customers
                .Where(c => c.Name.ToLower().Contains(query) || c.Id.ToString().Contains(query))
                .ToList();

            CustomerResults.Clear();
            foreach (var c in results) CustomerResults.Add(c);

            CustomerListView.ItemsSource = CustomerResults;
            CustomerListView.Visibility = CustomerResults.Any() ? Visibility.Visible : Visibility.Collapsed;
        }

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerListView.SelectedItem is Customer selectedCustomer)
            {
                NameBox.Text = selectedCustomer.Name;
                StreetBox.Text = selectedCustomer.Street;
                PostalCodeBox.Text = selectedCustomer.PostalCode;
                CityBox.Text = selectedCustomer.City;

                NewOffer = new Offer
                {
                    Customer = selectedCustomer,
                    CustomerId = selectedCustomer.Id,
                    Items = new System.Collections.Generic.List<OfferItem>()
                };

                CustomerListView.Visibility = Visibility.Collapsed;
                CustomerSearchBox.Text = string.Empty;
            }
        }

        // ===============================
        // PRODUCTEN TOEVOEGEN
        // ===============================
        private async void ShowMachineDialog_Click(object sender, RoutedEventArgs e)
        {
            var machines = await _context.Machines.ToListAsync();

            var dialog = new ContentDialog
            {
                Title = "Selecteer een Machine",
                PrimaryButtonText = "Toevoegen",
                CloseButtonText = "Annuleren",
                XamlRoot = this.XamlRoot
            };

            var listView = new ListView
            {
                ItemsSource = machines,
                DisplayMemberPath = "Name",
                SelectionMode = ListViewSelectionMode.Single,
                Height = 200
            };

            dialog.Content = listView;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary && listView.SelectedItem is Machine selectedMachine)
            {
                Items.Add(new OfferItem
                {
                    ProductName = selectedMachine.Name,
                    ProductNumber = selectedMachine.ArticleNumber,
                    Quantity = 1,
                    UnitPrice = selectedMachine.LeasePrice
                });
            }
        }

        private async void ShowCoffeeBeanDialog_Click(object sender, RoutedEventArgs e)
        {
            var coffeeBeans = await _context.CoffeeBeans.ToListAsync();

            var dialog = new ContentDialog
            {
                Title = "Selecteer een Koffieboon",
                PrimaryButtonText = "Toevoegen",
                CloseButtonText = "Annuleren",
                XamlRoot = this.XamlRoot
            };

            var listView = new ListView
            {
                ItemsSource = coffeeBeans,
                DisplayMemberPath = "Name",
                SelectionMode = ListViewSelectionMode.Single,
                Height = 200
            };

            dialog.Content = listView;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary && listView.SelectedItem is CoffeeBean selectedBean)
            {
                Items.Add(new OfferItem
                {
                    ProductName = selectedBean.Name,
                    ProductNumber = selectedBean.ArticleNumber,
                    Quantity = 1,
                    UnitPrice = selectedBean.PricePerKg
                });
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is OfferItem item)
                Items.Remove(item);
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RepeatButton btn && btn.DataContext is OfferItem item)
                item.Quantity++;
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RepeatButton btn && btn.DataContext is OfferItem item && item.Quantity > 0)
                item.Quantity--;
        }

        // ===============================
        // OFFER OPSLAAN
        // ===============================
        private async void SaveOffer_Click(object sender, RoutedEventArgs e)
        {
            if (NewOffer == null || NewOffer.Customer == null)
            {
                var dialog = new ContentDialog
                {
                    Title = "Fout",
                    Content = "Selecteer eerst een klant.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
                return;
            }

            try
            {
                // Genereer nummers
                NewOffer.OfferNumber = $"OFF-{DateTime.Now:yyyyMMddHHmmss}";
                NewOffer.ContractNumber = $"CN-{DateTime.Now:yyyyMMddHHmmss}";
                NewOffer.CustomerNumber = $"CUST-{DateTime.Now:yyyyMMddHHmmss}";

                // Vul overige velden
                NewOffer.Date = DateTime.Now;
                NewOffer.ValidUntil = ValidUntilPicker.Date.DateTime;
                NewOffer.PaymentTerms = $"Betaling binnen {PaymentDaysBox.Text} dagen na factuurdatum.";
                NewOffer.DeliveryTerms = $"Levering binnen {DeliveryDaysBox.Text} werkdagen na akkoord.";
                NewOffer.ExtraConditions = ExtraConditionsBox.Text;
                NewOffer.ContactPerson = ContactPersonBox.Text;
                NewOffer.SignatureName = SignatureNameBox.Text;

                NewOffer.Items = Items.ToList();

                // Opslaan
                _context.Offers.Add(NewOffer);
                await _context.SaveChangesAsync();

                // Terug of navigeren
                Frame.GoBack();
            }
            catch (DbUpdateException ex)
            {
                var dialog = new ContentDialog
                {
                    Title = "Database fout",
                    Content = ex.InnerException?.Message ?? ex.Message,
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
            }
        }
    }
}
