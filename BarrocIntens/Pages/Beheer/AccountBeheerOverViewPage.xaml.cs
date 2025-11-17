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
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.WebUI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.Pages.Beheer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountBeheerOverViewPage : Page
    {
        public AccountBeheerOverViewPage()
        {
            InitializeComponent();
            LoadChat(); 
        }

        //Load all Users in AccountListView
        private void LoadChat()
        {
            using var db = new AppDbContext();

            var user = db.Users
                .ToList();

            AccountListView.ItemsSource = user;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        //Navigate to AccountBeheerEditPage with UserId
        private void AccountListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var userId = (User)e.ClickedItem;

            Frame.Navigate(typeof(AccountBeheerEditPage), userId);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void UserSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchQuery = userSearchTextBox.Text;

            using var db = new AppDbContext();

            AccountListView.ItemsSource = db.Users
                .Where(c => c.Username.Contains(searchQuery) || c.Role.Contains(searchQuery))
                .OrderByDescending(c => c.Username)
                .ToList();

        }

        private void AccountCreate_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountBeheerCreate));
        }
            
        // Remove User Account
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var button = sender as Button;
            var userId = button?.DataContext as User;

            if (userId != null)
            {
                //Show Pop-Up Message
                var confirmDialog = new ContentDialog
                {
                    Title = "Weet je het zeker?",
                    Content = "Weet je zeker dat je deze gebruiker wilt verwijderen?",
                    PrimaryButtonText = "Ja",
                    CloseButtonText = "Nee",
                    XamlRoot = this.XamlRoot
                };

                var result = await confirmDialog.ShowAsync();

                //If oke
                if (result == ContentDialogResult.Primary)
                {
                    db.Users.Remove(userId);
                    db.SaveChanges();
                    LoadChat();
                    var dialog = new ContentDialog
                    {
                        Title = "Verwijderd!",
                        Content = "Gebruiker is verwijderd.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };

                    await dialog.ShowAsync();     
                }
            }
        }
    }
}
