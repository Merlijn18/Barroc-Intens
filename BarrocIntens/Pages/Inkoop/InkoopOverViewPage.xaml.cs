using BarrocIntens.Data;
using BarrocIntens.Models;
using BarrocIntens.Pages.Inlog;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BarrocIntens.Pages.Inkoop
{
    public sealed partial class InkoopOverViewPage : Page
    {
        private ObservableCollection<Product> Products { get; set; }
        private ObservableCollection<Bestelling> Bestellingen { get; set; }

        public InkoopOverViewPage()
        {
            this.InitializeComponent();

            LoadProducts();
            LoadBestellingen();
        }

        // --------------------
        // PRODUCTEN LADEN
        // --------------------
        private void LoadProducts()
        {
            using var db = new AppDbContext();
            var productList = db.Products.ToList();
            Products = new ObservableCollection<Product>(productList);
            ProductListView.ItemsSource = Products;
        }

        // --------------------
        // BESTELLINGEN LADEN
        // --------------------
        private void LoadBestellingen()
        {
            using var db = new AppDbContext();
            var bestellingenList = db.Bestellingen
                                     .OrderByDescending(b => b.OrderDate)
                                     .ToList();

            Bestellingen = new ObservableCollection<Bestelling>(bestellingenList);
            BestellingenListView.ItemsSource = Bestellingen;
        }

        // --------------------
        // LOGOUT
        // --------------------
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        // --------------------
        // TERUG
        // --------------------
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        // --------------------
        // PRODUCT ZOEKEN
        // --------------------
        private void productSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchQuery = productSearchTextBox.Text;

            using var db = new AppDbContext();
            var filteredProducts = db.Products
                                     .Where(p => p.Productname.Contains(searchQuery) ||
                                                 p.Stock.ToString().Contains(searchQuery))
                                     .OrderByDescending(p => p.Productname)
                                     .ToList();

            Products = new ObservableCollection<Product>(filteredProducts);
            ProductListView.ItemsSource = Products;
        }

        // --------------------
        // PRODUCTEN HELPERS
        // --------------------
        private void ShowProducts_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();
            var lowStockProducts = db.Products
                                     .Where(p => p.Stock < 3)
                                     .ToList();

            if (lowStockProducts.Any())
            {
                LowStockWarning.Text = "De volgende producten zijn laag in voorraad:\n" +
                                       string.Join("\n", lowStockProducts.Select(p => $"{p.Productname} (Stock: {p.Stock})"));

                LowStockWarning.Visibility = Visibility.Visible;
            }
            else
            {
                LowStockWarning.Visibility = Visibility.Collapsed;
            }

            ProductListView.Visibility = Visibility.Visible;
            HideProductsButton.Visibility = Visibility.Visible;
            ShowProductsButton.Visibility = Visibility.Collapsed;
        }

        private void HideProductsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductListView.Visibility = Visibility.Collapsed;
            HideProductsButton.Visibility = Visibility.Collapsed;
            ShowProductsButton.Visibility = Visibility.Visible;
            LowStockWarning.Visibility = Visibility.Collapsed;
        }

        // --------------------
        // NIEUWE BESTELLING
        // --------------------
        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BestellingAanmaken));
        }

        private void LeverancierBeheer_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LeverancierBeheer));
        }

    
        private async void GoedkeurenButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag == null) return;

            int orderId = int.Parse(button.Tag.ToString());

            using var db = new AppDbContext();
            var order = await db.Bestellingen.FindAsync(orderId); // Zorg dat Tag overeenkomt met Id in database

            if (order == null) return;

            order.Status = "Goedgekeurd";
            await db.SaveChangesAsync();

            // Toevoegen aan helper
            BestellingHelper.AddNotification(order);

            // UI updaten via Dispatcher
            _ = DispatcherQueue.TryEnqueue(() =>
            {
                Bestellingen.Remove(order); // verwijdert bestelling uit ListView
            });
        }
    }
}
