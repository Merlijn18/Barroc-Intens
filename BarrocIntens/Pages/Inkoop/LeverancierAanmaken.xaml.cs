using BarrocIntens.Data;
using BarrocIntens.Models;
using BarrocIntens.Pages.Inlog;
using Microsoft.EntityFrameworkCore;
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
    public sealed partial class LeverancierAanmaken : Page
    {
        public LeverancierAanmaken()
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



        private async void SaveNewSupplier_Click(object sender, RoutedEventArgs e)
        {
            // Check of verplichte velden zijn ingevuld
            if (string.IsNullOrWhiteSpace(LeverancierNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(ContactpersoonTextBox.Text) ||
                string.IsNullOrWhiteSpace(TelefoonnummerTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(AdresTextBox.Text))
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

            if (!int.TryParse(TelefoonnummerTextBox.Text.Trim(), out int telefoonnummer))
            {
                ContentDialog invalidPhoneDialog = new ContentDialog
                {
                    Title = "Ongeldige invoer",
                    Content = "Telefoonnummer moet een geheel getal zijn.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.XamlRoot
                };

                await invalidPhoneDialog.ShowAsync();
                return;
            }


            if (!IsValidEmail(EmailTextBox.Text.Trim()))
            {
                ContentDialog invalidEmailDialog = new ContentDialog
                {
                    Title = "Ongeldig e-mailadres",
                    Content = "Vul een geldig e-mailadres in.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.XamlRoot
                };

                await invalidEmailDialog.ShowAsync();
                return;
            }

            using var db = new AppDbContext();

            var newSupplier = new Leverancier
            {
                Leveranciernaam = LeverancierNameTextBox.Text.Trim(),
                Contactpersoon = ContactpersoonTextBox.Text.Trim(),
                Telefoonnummer = telefoonnummer,
                Email = EmailTextBox.Text.Trim(),
                Adres = AdresTextBox.Text.Trim(),
            };

            db.Leveranciers.Add(newSupplier);
            db.SaveChanges();

            // Formulier verbergen
            LeverancierFormPanel.Visibility = Visibility.Collapsed;

            // Navigeren naar overzicht
            Frame.Navigate(typeof(LeverancierBeheer));

            // Bevestiging tonen
            ContentDialog confirmationDialog = new ContentDialog
            {
                Title = "Leverancier toegevoegd",
                Content = "De nieuwe leverancier is succesvol opgeslagen!",
                CloseButtonText = "OK",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.XamlRoot
            };

            await confirmationDialog.ShowAsync();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
