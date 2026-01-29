using BarrocIntens.Data;
using BarrocIntens.Models;
using BarrocIntens.Pages.Inlog;
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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.Pages.Inkoop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Materials : Page
    {
        public Materials()
        {
            InitializeComponent();
            LoadProducts();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            User.LoggedInUser = null;
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        private void LoadProducts()
        {
            using var db = new AppDbContext();
            var productList = db.Products.ToList();

        }

        private void AddMaterialButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowMaterialsButton_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            // 1. Haal ALLE producten op om in de lijst te tonen
            var allProducts = db.Products.ToList();
            ProductListView.ItemsSource = allProducts;

            // 2. Logica voor de voorraad-waarschuwing (die had je al, heel goed!)
            var lowStockProducts = allProducts.Where(p => p.Stock < 3).ToList();

            if (lowStockProducts.Any())
            {
                LowStockWarning.Text = "⚠️ Lage voorraad:\n" +
                                       string.Join("\n", lowStockProducts.Select(p => $"- {p.Productname} ({p.Stock} stuks)"));
                LowStockWarning.Visibility = Visibility.Visible;
            }
            else
            {
                LowStockWarning.Visibility = Visibility.Collapsed;
            }

            // 3. UI elementen op de juiste manier tonen/verbergen
            ProductListView.Visibility = Visibility.Visible;
            HideMaterialsButton.Visibility = Visibility.Visible;
            ShowMaterialsButton.Visibility = Visibility.Collapsed;

            // Als je de PlaceholderText uit mijn vorige XAML-voorbeeld gebruikt:
            if (PlaceholderText != null) PlaceholderText.Visibility = Visibility.Collapsed;
        }

        private void HideMaterialsButton_Click(object sender, RoutedEventArgs e)
        {
            // Alles weer verbergen
            ProductListView.Visibility = Visibility.Collapsed;
            HideMaterialsButton.Visibility = Visibility.Collapsed;
            ShowMaterialsButton.Visibility = Visibility.Visible;
            LowStockWarning.Visibility = Visibility.Collapsed;

            if (PlaceholderText != null) PlaceholderText.Visibility = Visibility.Visible;
        }

    }

}
