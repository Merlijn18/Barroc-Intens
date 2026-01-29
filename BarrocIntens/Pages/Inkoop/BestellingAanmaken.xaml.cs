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

        private Dictionary<string, double> CoffeePrices = new Dictionary<string, double>()
        {
            { "Espresso Beneficio", 18.50 },
            { "Yellow Bourbon Brasil", 20.00 },
            { "Espresso Roma", 19.00 },
            { "Red Honey Honduras", 21.25 }
        };

        public BestellingAanmaken()
        {
            InitializeComponent();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            User.LoggedInUser = null;
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
            CoffeeBeansComboBox.SelectedIndex = -1;
            AantalKiloTextBox.Text = string.Empty;
            SupplierNameTextBox.Text = string.Empty;
            QuantityTextBox.Text = string.Empty;
            UnitPriceTextBox.Text = string.Empty;
            StatusComboBox.SelectedIndex = -1;
            ExpectedDeliveryDatePicker.Date = null;
        }

        // ✔️ Automatisch prijs invullen bij selectie van product
        private void ProductNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedProduct =
                (ProductNameComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedProduct != null && ProductPrices.ContainsKey(selectedProduct))
            {
                UnitPriceTextBox.Text = ProductPrices[selectedProduct].ToString("0.00");
            }
            else
            {
                UnitPriceTextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(selectedProduct) || selectedProduct == "Geen")
            {
                ProductPrijsPanel.Visibility = Visibility.Collapsed;
                UnitPriceTextBox.Text = "";

            }
            else
            {
                ProductPrijsPanel.Visibility = Visibility.Visible;
            }
        }

        // ✔️ Als iemand koffiebonen kiest, kan je hier later logica toevoegen
        private void CoffeeBeansComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCoffeebean = (CoffeeBeansComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(selectedCoffeebean) || selectedCoffeebean == "Geen")
            {
                // Verberg AantalKiloPanel en wis tekst
                AantalKiloPanel.Visibility = Visibility.Collapsed;
                AantalKiloTextBox.Text = "";

                // Verberg PrijsPanel en wis prijs
                PrijsPanel.Visibility = Visibility.Collapsed;
                PrijsPerKiloTextBlock.Text = "";
            }
            else
            {
                // Toon AantalKiloPanel
                AantalKiloPanel.Visibility = Visibility.Visible;

                // Toon PrijsPanel en vul prijs
                PrijsPanel.Visibility = Visibility.Visible;

                if (CoffeePrices.ContainsKey(selectedCoffeebean))
                {
                    PrijsPerKiloTextBlock.Text = CoffeePrices[selectedCoffeebean].ToString("0.00") + " €";
                }
                else
                {
                    PrijsPerKiloTextBlock.Text = "0.00 €";
                }
            }
        }

        private async void SaveNewOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ProductNameComboBox.SelectedIndex < 0 ||
                CoffeeBeansComboBox.SelectedIndex < 0 ||
                string.IsNullOrWhiteSpace(AantalKiloTextBox.Text) ||
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

            if (!decimal.TryParse(AantalKiloTextBox.Text.Trim(), out decimal aantalKilo))
            {
                ContentDialog invalidKiloDialog = new ContentDialog
                {
                    Title = "Ongeldige invoer",
                    Content = "Aantal kilogram moet een getal zijn.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.XamlRoot
                };

                await invalidKiloDialog.ShowAsync();
                return;
            }

            string selectedProduct =
                (ProductNameComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            string selectedCoffeeBeans =
                (CoffeeBeansComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            using var db = new AppDbContext();

            var newOrder = new Order
            {
                Productname = selectedProduct,
                CoffeeBeans = selectedCoffeeBeans,     // ✔️ toegevoegd
                AantalKilo = aantalKilo,                    // ✔️ toegevoegd
                Suppliername = SupplierNameTextBox.Text.Trim(),
                OrderQuantity = quantity,
                UnitPrice = unitPrice,
                ExpectedDeliveryDate = ExpectedDeliveryDatePicker.Date.HasValue
                ? DateOnly.FromDateTime(ExpectedDeliveryDatePicker.Date.Value.DateTime)
                : DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                OrderDate = DateTime.Now
            };

            db.Bestellingen.Add(newOrder);
            db.SaveChanges();

            OrderFormPanel.Visibility = Visibility.Collapsed;
            Frame.Navigate(typeof(InkoopOverViewPage));

            ContentDialog confirmationDialog = new ContentDialog
            {
                Title = "Order Toegevoegd",
                Content = "De nieuwe bestelling is succesvol opgeslagen!",
                CloseButtonText = "OK",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.XamlRoot
            };

            await confirmationDialog.ShowAsync();
        }
    }
}
