using BarrocIntens.Data;
using BarrocIntens.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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
            InitializeComponent();
            DataContext = this;
        }

        // ===============================
        // NAVIGATIE
        // ===============================
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
            string query = CustomerSearchBox.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(query))
            {
                CustomerListView.Visibility = Visibility.Collapsed;
                CustomerResults.Clear();
                NewCustomerPanel.Visibility = Visibility.Collapsed;
                return;
            }

            var results = _context.Customers
                .Where(c => c.Name.ToLower().Contains(query) || c.Id.ToString().Contains(query))
                .ToList();

            CustomerResults.Clear();
            foreach (var c in results)
                CustomerResults.Add(c);

            CustomerListView.ItemsSource = CustomerResults;
            CustomerListView.Visibility = CustomerResults.Any() ? Visibility.Visible : Visibility.Collapsed;
            NewCustomerPanel.Visibility = CustomerResults.Any() ? Visibility.Collapsed : Visibility.Visible;
            NewCustomerNameBox.Text = CustomerSearchBox.Text;
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
                NewCustomerPanel.Visibility = Visibility.Collapsed;
                CustomerSearchBox.Text = string.Empty;
            }
        }

        // ===============================
        // ITEMS HANDLING
        // ===============================
        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is OfferItem item)
            {
                item.Quantity++;
                RefreshItems();
            }
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is OfferItem item)
            {
                if (item.Quantity > 1)
                    item.Quantity--;

                RefreshItems();
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is OfferItem item)
            {
                Items.Remove(item);
                RefreshItems();
            }
        }

        private void RefreshItems()
        {
            ItemsControlProducts.ItemsSource = null;
            ItemsControlProducts.ItemsSource = Items;
        }

        // ===============================
        // MACHINE SELECTOR
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
                    UnitPrice = selectedMachine.LeasePrice,
                });
            }
        }

        // ===============================
        // COFFEE SELECTOR
        // ===============================
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
                });
            }
        }

        // ===============================
        // OFFER OPSLAAN
        // ===============================
        private async void SaveOffer_Click(object sender, RoutedEventArgs e)
        {
            if (!Items.Any())
            {
                await new ContentDialog
                {
                    Title = "Geen producten",
                    Content = "Voeg minimaal één product toe.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                return;
            }

            Customer customer = null;

            if (NewCustomerPanel.Visibility == Visibility.Visible)
            {
                customer = new Customer
                {
                    Name = NewCustomerNameBox.Text,
                    Street = NewCustomerStreetBox.Text,
                    PostalCode = NewCustomerPostalCodeBox.Text,
                    City = NewCustomerCityBox.Text,
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }
            else if (NewOffer?.Customer != null)
            {
                customer = NewOffer.Customer;
            }
            else
            {
                await new ContentDialog
                {
                    Title = "Fout",
                    Content = "Selecteer eerst een klant.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                return;
            }

            NewOffer ??= new Offer();
            NewOffer.Customer = customer;
            NewOffer.CustomerId = customer.Id;
            NewOffer.Items = Items.ToList();

            NewOffer.OfferNumber = $"OFF-{DateTime.Now:yyyyMMddHHmmss}";
            NewOffer.ContractNumber = $"CN-{DateTime.Now:yyyyMMddHHmmss}";
            NewOffer.CustomerNumber = $"CUST-{DateTime.Now:yyyyMMddHHmmss}";
            NewOffer.Date = DateTime.Now;
            NewOffer.ValidUntil = ValidUntilPicker.Date.DateTime;
            NewOffer.PaymentTerms = $"Betaling binnen {PaymentDaysBox.Text} dagen.";
            NewOffer.DeliveryTerms = $"Levering binnen {DeliveryDaysBox.Text} werkdagen.";
            NewOffer.ExtraConditions = ExtraConditionsBox.Text;
            NewOffer.ContactPerson = ContactPersonBox.Text;
            NewOffer.SignatureName = SignatureNameBox.Text;

            try
            {
                _context.Offers.Add(NewOffer);
                await _context.SaveChangesAsync();
                Frame.GoBack();
            }
            catch (DbUpdateException ex)
            {
                await new ContentDialog
                {
                    Title = "Database fout",
                    Content = ex.InnerException?.Message ?? ex.Message,
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
            }
        }
    }
}
