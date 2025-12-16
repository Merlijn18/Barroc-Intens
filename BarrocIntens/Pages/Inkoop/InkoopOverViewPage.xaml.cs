using BarrocIntens.Data;
using BarrocIntens.Models;
using BarrocIntens.Pages.Inlog;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
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
            User.LoggedInUser = null;
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
        private void ShowMaterials_Click(object sender, RoutedEventArgs e)
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
            HideMaterialsButton.Visibility = Visibility.Visible;
            ShowMaterialsButton.Visibility = Visibility.Collapsed;
        }

        private void HideMaterialsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductListView.Visibility = Visibility.Collapsed;
            HideMaterialsButton.Visibility = Visibility.Collapsed;
            ShowMaterialsButton.Visibility = Visibility.Visible;
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
            var order = await db.Bestellingen.FindAsync(orderId);

            if (order == null) return;

            // Status aanpassen
            order.Status = "Goedgekeurd";
            await db.SaveChangesAsync();

            // Toevoegen aan helper
            BestellingHelper.AddNotification(order);

            // UI bijwerken
            _ = DispatcherQueue.TryEnqueue(() =>
            {
                // 1. Update de ListView binding voor deze bestelling
                var itemContainer = BestellingenListView.ContainerFromItem(order) as ListViewItem;
                if (itemContainer != null)
                {
                    // Zoek de TextBlock en Button binnen het item
                    var statusTextBlock = FindChild<TextBlock>(itemContainer, tb => tb.Text == "Aangevraagd"); // of huidige status
                    if (statusTextBlock != null)
                        statusTextBlock.Text = "Goedgekeurd";

                    var goedkeurenButton = FindChild<Button>(itemContainer, b => b == button);
                    if (goedkeurenButton != null)
                    {
                        goedkeurenButton.Content = "Goedgekeurd";
                        goedkeurenButton.IsEnabled = false;
                    }
                }
            });

            // ✅ Toon bevestigingsmelding
            var confirmationDialog = new ContentDialog
            {
                Title = "Bestelling Goedgekeurd",
                Content = "Je melding is goedgekeurd en doorgegeven aan de leverancierafdeling.",
                CloseButtonText = "OK",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.XamlRoot
            };

            await confirmationDialog.ShowAsync();
        }

        // Hulpfunctie om child controls in ListViewItem te vinden
        private T FindChild<T>(DependencyObject parent, Func<T, bool> predicate) where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild && predicate(typedChild))
                    return typedChild;

                var result = FindChild(child, predicate);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void ShowMachinesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowCoffeeBeansButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
