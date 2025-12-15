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
using Windows.Security.Authentication.OnlineId;
using Windows.Security.EnterpriseData;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.Pages.Beheer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountBeheerEditPage : Page
    {
        private int _userId;
        public AccountBeheerEditPage()
        {
            InitializeComponent();
        }

        // Get UserInformation 
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var user = (User)e.Parameter;

            using var db = new AppDbContext();
            _userId = user.Id;

            //Shows information in TextBox
            NameTextBox.Text = user.Username;
            EmailTextBox.Text = user.Email;
            RoleDropDownButton.Content = user.Role;

            //Saves Role in tag
            RoleDropDownButton.Tag = user.Role;
        }

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

        private void SaveChange_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u => u.Id == _userId);

            if (user != null)
            {
                user.Username = NameTextBox.Text.Trim();
                user.Email = EmailTextBox.Text.Trim();
                user.Role = RoleDropDownButton.Tag as string;

                var enterdPassword = PasswordTextBox.Password.Trim();
                if (!string.IsNullOrEmpty(enterdPassword))
                {
                    var hashedPassowrd = BCrypt.Net.BCrypt.HashPassword(enterdPassword);
                    user.Password = hashedPassowrd;
                }

                db.SaveChanges();

                //Pop-Up Message 
                var dialog = new ContentDialog
                {
                    Title = "Gelukt!",
                    Content = "Gebruiker is bijgewerkt.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };

                _ = dialog.ShowAsync();

                Frame.GoBack();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();

        }
    }
}
