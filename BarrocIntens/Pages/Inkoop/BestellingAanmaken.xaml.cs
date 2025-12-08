using BarrocIntens.Data;
using BarrocIntens.Models;
using BarrocIntens.Pages.Inlog;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace BarrocIntens.Pages.Inkoop
{
    public sealed partial class BestellingAanmaken : Page
    {
        // ✔️ Dictionary met vaste prijzen
        private readonly Dictionary<string, decimal> ProductPrices = new()
        {
            { "Barroc Intens Italian Light", 999.00m },
            { "Barroc Intens Italian", 1299.00m },
            { "Barroc Intens Italian Deluxe", 1599.00m },
            { "Barroc Intens Italian Deluxe Special", 1999.00m }
        };

        public BestellingAanmaken()
        {
            InitializeComponent();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderFormPanel.Visibility = Visibility.Visible;

            // Velden leegmaken
            ProductNameComboBox.SelectedIndex = -1;
            SupplierNameTextBox.Text = string.Empty;
            QuantityTextBox.Text = string.Empty;
            UnitPriceTextBox.Text = string.Empty;
            StatusComboBox.SelectedIndex = -1;
            ExpectedDeliveryDatePicker.Date = null;
        }

        // ✔️ Automatisch prijs invullen bij selectie
        private void ProductNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedProduct = (ProductNameComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedProduct != null && ProductPrices.ContainsKey(selectedProduct))
            {
                UnitPriceTextBox.Text = ProductPrices[selectedProduct].ToString("0.00");
            }
        }

        private async void SaveNewOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ProductNameComboBox.SelectedIndex < 0 ||
                string.IsNullOrWhiteSpace(SupplierNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(QuantityTextBox.Text) ||
                string.IsNullOrWhiteSpace(UnitPriceTextBox.Text) ||
                StatusComboBox.SelectedIndex < 0)
            {
                ContentDialog warningDialog = new ContentDialog
                {
                    Title = "Ongeldige invoer",
                    Content = "Vul alle verplichte velden in voordat je opslaat!",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.XamlRoot
                };

                await warningDialog.ShowAsync();
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text.Trim(), out int quantity))
            {
                ContentDialog invalidNumberDialog = new ContentDialog
                {
                    Title = "Ongeldige invoer",
                    Content = "Quantity moet een geheel getal zijn.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.XamlRoot
                };

                await invalidNumberDialog.ShowAsync();
                return;
            }

            if (!decimal.TryParse(UnitPriceTextBox.Text.Trim(), out decimal unitPrice))
            {
                ContentDialog invalidPriceDialog = new ContentDialog
                {
                    Title = "Ongeldige invoer",
                    Content = "Unit Price moet een geldig getal zijn.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.XamlRoot
                };

                await invalidPriceDialog.ShowAsync();
                return;
            }

            string selectedProduct =
                (ProductNameComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            using var db = new AppDbContext();

            var newOrder = new Bestelling
            {
                Productname = selectedProduct,
                Suppliername = SupplierNameTextBox.Text.Trim(),
                OrderQuantity = quantity,
                UnitPrice = unitPrice,
                ExpectedDeliveryDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                OrderDate = DateTime.Now,
            };

            db.Bestellingen.Add(newOrder);
            db.SaveChanges();

            OrderFormPanel.Visibility = Visibility.Collapsed;
            Frame.Navigate(typeof(InkoopOverViewPage));

            ContentDialog confirmationDialog = new ContentDialog
            {
                Title = "Bestelling Toegevoegd",
                Content = "De nieuwe bestelling is succesvol opgeslagen!",
                CloseButtonText = "OK",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.XamlRoot
            };

            await confirmationDialog.ShowAsync();
        }
    }
}
