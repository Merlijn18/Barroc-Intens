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
    public sealed partial class BestellingAanmaken : Page
    {
        public BestellingAanmaken()
        {
            InitializeComponent();


        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        // Back knop
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            // Maak het formulier zichtbaar
            OrderFormPanel.Visibility = Visibility.Visible;

            // Velden leegmaken voor een nieuwe bestelling
            ProductNameTextBox.Text = string.Empty;
            SupplierNameTextBox.Text = string.Empty;
            QuantityTextBox.Text = string.Empty;
            UnitPriceTextBox.Text = string.Empty;
            StatusComboBox.SelectedIndex = -1;
            NotesTextBox.Text = string.Empty;
            ExpectedDeliveryDatePicker.Date = null;

        }

        private async void SaveNewOrder_Click(object sender, RoutedEventArgs e)
        {
            // Controleer of alle verplichte velden zijn ingevuld
            if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(SupplierNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(QuantityTextBox.Text) ||
                string.IsNullOrWhiteSpace(UnitPriceTextBox.Text) ||
                StatusComboBox.SelectedIndex < 0)
            {
                // Toon waarschuwing als iets niet is ingevuld
                ContentDialog warningDialog = new ContentDialog
                {
                    Title = "Ongeldige invoer",
                    Content = "Vul alle verplichte velden in voordat je opslaat!",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.XamlRoot
                };

                await warningDialog.ShowAsync();
                return; // Stop de methode zodat er niet wordt opgeslagen
            }

            // Alles is ingevuld, sla de bestelling op
            using var db = new AppDbContext();

            var newOrder = new Bestelling
            {
                Productname = ProductNameTextBox.Text.Trim(),
                Suppliername = SupplierNameTextBox.Text.Trim(),
                OrderQuantity = int.Parse(QuantityTextBox.Text.Trim()),
                UnitPrice = decimal.Parse(UnitPriceTextBox.Text.Trim()),
                Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Remark = NotesTextBox.Text.Trim(),
                OrderDate = DateTime.Now,
            };

            db.Bestellingen.Add(newOrder);
            db.SaveChanges();

            // Formulier verbergen
            OrderFormPanel.Visibility = Visibility.Collapsed;

            // Eventueel navigeren
            Frame.Navigate(typeof(InkoopOverViewPage));

            // Toon bevestigingspopup
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

