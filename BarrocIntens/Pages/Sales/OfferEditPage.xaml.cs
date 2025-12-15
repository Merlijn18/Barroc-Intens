using BarrocIntens.Data;
using BarrocIntens.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BarrocIntens.Pages.Sales
{
    public sealed partial class OfferEditPage : Page
    {
        private readonly AppDbContext _context = new();

        public Offer SelectedOffer { get; set; }
        public ObservableCollection<Customer> CustomerResults { get; set; } = new();
        public ObservableCollection<OfferItem> Items { get; set; } = new();

        public OfferEditPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is not int offerId) return;

            SelectedOffer = await _context.Offers
                .Include(o => o.Customer)
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == offerId);

            if (SelectedOffer == null) return;

            // Vul klantvelden
            if (SelectedOffer.Customer != null)
            {
                NameBox.Text = SelectedOffer.Customer.Name;
                StreetBox.Text = SelectedOffer.Customer.Street;
                PostalCodeBox.Text = SelectedOffer.Customer.PostalCode;
                CityBox.Text = SelectedOffer.Customer.City;
            }

            // Vul offertevelden
            //PaymentTermsBox.Text = SelectedOffer.PaymentTerms;
            //DeliveryTermsBox.Text = SelectedOffer.DeliveryTerms;
            ValidUntilPicker.Date = SelectedOffer.ValidUntil ?? DateTimeOffset.Now;
            ExtraConditionsBox.Text = SelectedOffer.ExtraConditions;
            ContactPersonBox.Text = SelectedOffer.ContactPerson;
            SignatureNameBox.Text = SelectedOffer.SignatureName;

            Items = new ObservableCollection<OfferItem>(SelectedOffer.Items ?? new System.Collections.Generic.List<OfferItem>());
            DataContext = this;
        }

        private void Back_Click(object sender, RoutedEventArgs e) => Frame.GoBack();

        private void SaveOffer_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(PaymentDaysBox.Text, out int paymentDays))
                SelectedOffer.PaymentTerms = $"Betaling binnen {paymentDays} dagen na factuurdatum.";

            if (int.TryParse(DeliveryDaysBox.Text, out int deliveryDays))
                SelectedOffer.DeliveryTerms = $"Levering binnen {deliveryDays} werkdagen na akkoord.";

            SelectedOffer.Items = Items.ToList();
            _context.SaveChanges();
            Frame.GoBack();
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is OfferItem item)
                Items.Remove(item);
        }

        // Quantiteit aanpassen
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

        // Machines toevoegen
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
                    UnitPrice = selectedMachine.LeasePrice,
                    OfferId = SelectedOffer.Id
                });
            }
        }

        // CoffeeBeans toevoegen
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
                    UnitPrice = selectedBean.PricePerKg,
                    OfferId = SelectedOffer.Id
                });
            }
        }

        // KLANT ZOEKEN
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

                SelectedOffer.Customer = selectedCustomer;

                CustomerListView.Visibility = Visibility.Collapsed;
                CustomerSearchBox.Text = string.Empty;
            }
        }
    }
}
