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

        //DropDownMenu for Role
        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuFlyoutItem;
            if (item != null)
            {
               
                string selectedRole = item.Text; 

                //Save Choices
                RoleDropDownButton.Tag = item.Text;

                //Shows Choices
                RoleDropDownButton.Content = selectedRole;
            }
        }

        private void Create_Account_Click(object sender, RoutedEventArgs e)
        {

            var enterdUsername = NameTextBox.Text.Trim();
            var enterdtEmail = EmailTextBox.Text.Trim();
            var enterdPassword = PasswordTextBox.Password.Trim();
            var enteredPasswordConfirmation = ConfirmPasswordBox.Password.Trim();
            var selectedRole = RoleDropDownButton.Tag as string;

            if (string.IsNullOrEmpty(enterdUsername))
            {
                ShowError("Gebruikersnaam is niet ingevuld!");
                return;
            }
            
            if (string.IsNullOrEmpty(enterdtEmail))
            {
                ShowError("Email is niet ingevuld!");
                return;
            }

            if (string.IsNullOrEmpty(enterdPassword))
            {
                ShowError("Wachtwoord is niet ingevuld!");
            }

            if (string.IsNullOrEmpty(selectedRole))
            {
                ShowError("De Role is niet ingevuld!");
                return;
            }


            if (enterdPassword.Length < 6)
            {
                ShowError("Wachtwoord moet minimaal 6 tekens bevatten!");
                return;
            }

            if (enterdPassword != enteredPasswordConfirmation)
            {
                ShowError("Wachtwoorden Matchen niet!");
                ConfirmPasswordBox.Password = string.Empty;
                return;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(enterdPassword);
            using var db = new AppDbContext();
                var user = new User
                {
                    Username = enterdUsername,
                    Email = enterdtEmail,
                    Password = hashedPassword,
                    Role = selectedRole
                };


                db.Users.Add(user);
                db.SaveChanges();

                //Show Pop-Up Message
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

        private void PasswordTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }

       private void ShowError(string message)
        {
            ErrorMessageText.Text = message;
        }
    }
}
