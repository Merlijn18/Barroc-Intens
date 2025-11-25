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
        // ObservableCollections voor binding aan de UI
        private ObservableCollection<Product> Products { get; set; }
        private ObservableCollection<Bestelling> Bestellingen { get; set; }

        public InkoopOverViewPage()
        {
            this.InitializeComponent();

            LoadProducts();
            LoadBestellingen();
        }

        // Producten laden uit database
        private void LoadProducts()
        {
            using var db = new AppDbContext();
            var productList = db.Products.ToList();
            Products = new ObservableCollection<Product>(productList);
            ProductListView.ItemsSource = Products;
        }

        // Bestellingen laden uit database
        private void LoadBestellingen()
        {
            using var db = new AppDbContext();
            var bestellingenList = db.Bestellingen
                                     .OrderByDescending(b => b.OrderDate)
                                     .ToList();

            Bestellingen = new ObservableCollection<Bestelling>(bestellingenList);
            BestellingenListView.ItemsSource = Bestellingen;
        }

        // Logout knop
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        // Back knop
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        // Product search
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

        // Toon producten knop
        private void ShowProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductListView.Visibility = Visibility.Visible;
            HideProductsButton.Visibility = Visibility.Visible;
            ShowProductsButton.Visibility = Visibility.Collapsed;
        }

        // Verberg producten knop
        private void HideProductsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductListView.Visibility = Visibility.Collapsed;
            HideProductsButton.Visibility = Visibility.Collapsed;
            ShowProductsButton.Visibility = Visibility.Visible;
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BestellingAanmaken));
            
            
        }
    }
}
