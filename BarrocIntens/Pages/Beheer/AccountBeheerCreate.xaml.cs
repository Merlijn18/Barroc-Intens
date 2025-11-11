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

namespace BarrocIntens.Pages.Beheer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountBeheerCreate : Page
    {
        
        public AccountBeheerCreate()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuFlyoutItem;
            if (item != null)
            {
                string selectedRole = item.Text;
                RoleDropDownButton.Tag = item.Text;
                RoleDropDownButton.Content = selectedRole;
            }
        }
        private void Create_Account_Click(object sender, RoutedEventArgs e)
        {

            var InputUsername = NameTextBox.Text.Trim();
            var InputEmail = EmailTextBox.Text.Trim();
            var InputPassword = PasswordTextBox.Password.Trim();
            var InputRole = RoleDropDownButton.Tag as string;

            using var db = new AppDbContext();

            if (InputUsername != null && InputEmail != null && InputPassword != null && InputRole != null)
            {
                var user = new User
                {
                    Username = InputUsername,
                    Email = InputEmail,
                    Password = InputPassword,
                    Role = InputRole

                };


                db.Users.Add(user);
                db.SaveChanges();

                var dialog = new ContentDialog
                {
                    Title = "Gelukt!",
                    Content = "Gebruiker is Aangemaakt.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };

                _ = dialog.ShowAsync();

                Frame.GoBack();
            }
            else
            {
                MessageText.Text = "Username/Email of Wachtwoord is niet ingevuld!";
            }
            

        }


    }
}
