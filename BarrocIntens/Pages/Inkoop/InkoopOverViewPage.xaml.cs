using BarrocIntens.Data;
using BarrocIntens.Models;
using BarrocIntens.Pages.Inlog;
using Microsoft.UI;
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
        private ObservableCollection<Material> Products { get; set; }
        private ObservableCollection<Order> Bestellingen { get; set; }

        public InkoopOverViewPage()
        {
            this.InitializeComponent();

            LoadBestellingen();
        }

        // --------------------
        // PRODUCTEN LADEN
        // --------------------
      

        // --------------------
        // BESTELLINGEN LADEN
        // --------------------
        private void LoadBestellingen()
        {
            using var db = new AppDbContext();
            var bestellingenList = db.Bestellingen
                                     .OrderByDescending(b => b.OrderDate)
                                     .ToList();

            Bestellingen = new ObservableCollection<Order>(bestellingenList);
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

            Products = new ObservableCollection<Material>(filteredProducts);
        }

        // --------------------
        // PRODUCTEN HELPERS
        // --------------------
       

        
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
            if (sender is not Button button || button.Tag is not int orderId)
                return;

            using var db = new AppDbContext();
            var order = await db.Bestellingen.FindAsync(orderId);

            if (order == null)
                return;

            // Status opslaan
            order.Status = "Goedgekeurd";
            await db.SaveChangesAsync();

            BestellingHelper.AddNotification(order);

            // ✅ KNOP AANPASSEN
            button.Content = "Goedgekeurd";
            button.IsEnabled = false;
            button.Background = new SolidColorBrush(Colors.Gray);

            // Bevestiging tonen
            await new ContentDialog
            {
                Title = "Order Goedgekeurd",
                Content = "Je melding is goedgekeurd en doorgegeven aan de leverancierafdeling.",
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            }.ShowAsync();
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

        private void MateriaalBeheer_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Materials));
        }
    }
}
