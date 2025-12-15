using BarrocIntens.Data;
using BarrocIntens.Models;
using BarrocIntens.Pages.Beheer;
using BarrocIntens.Pages.Financien;
using BarrocIntens.Pages.Inkoop;
using BarrocIntens.Pages.Inlog;
using BarrocIntens.Pages.Maintenance;
using BarrocIntens.Pages.Sales;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.Pages.Inlog
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InlogOverViewPage : Page
    {
        public InlogOverViewPage()
        {
            InitializeComponent();
        }

        //Dev Inlog !Remove Before Publish!
        private void DevInlogButton_Click(object sender, RoutedEventArgs e)
        {
            var username = "Harry@gmail.com";
            var password = "123";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError("Een van de gegevens zijn niet ingevuld!");
                return;
            }

            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u =>
            u.Username.ToLower() == username.ToLower() || u.Email.ToLower() == username.ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                ShowError("⚠ ACCESS DENIED: Incorrect Wachtwoord!");

            }
            else
            {
                User.LoggedInUser = user;
                Frame.Navigate(typeof(BeheerOverViewPage));
            }
        }


        private async void AttemptLogin()
        {
            string enterdUsername = NameEmailTextBox.Text.Trim();
            string enterdPassword = PasswordTextBox.Password;

            if(string.IsNullOrEmpty(enterdUsername) || string.IsNullOrEmpty(enterdPassword))
            {
                ShowError("Een van de gegevens zijn niet ingevuld!");
                return;
            }


            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u =>
            u.Username.ToLower() == enterdUsername.ToLower() || u.Email.ToLower() == enterdUsername.ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(enterdPassword, user.Password))
            {
                ShowError("⚠ ACCESS DENIED: Incorrect Wachtwoord!");

                // Clear password field
                PasswordTextBox.Password = string.Empty;
                PasswordTextBox.Focus(FocusState.Programmatic);

            }
            else
            {
                User.LoggedInUser = user;

                if(user.Role == "Beheer")
                {
                    Frame.Navigate(typeof(BeheerOverViewPage));
                }
                if (user.Role == "Sales")
                {
                    Frame.Navigate(typeof(SalesOverViewPage));
                }
                if (user.Role == "Monteur")
                {
                    Frame.Navigate(typeof(MaintenanceOverviewPage));
                }
                if (user.Role == "Inkoop")
                {
                    Frame.Navigate(typeof(InkoopOverViewPage));
                }
                if (user.Role == "Financien")
                {
                    Frame.Navigate(typeof(FinancienOverViewPage));
                }
            }



        }
        private async void PasswordBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
               AttemptLogin();
            }
        }
        private void InlogButton_Click(object sender, RoutedEventArgs e)
        {
            AttemptLogin();
        }
        private void ShowError(string message)
        {
            ErrorMessage.Text = message;
        }
    }
}
