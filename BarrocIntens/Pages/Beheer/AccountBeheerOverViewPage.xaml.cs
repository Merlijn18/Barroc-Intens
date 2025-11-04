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

        private void AccountListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var userId = (User)e.ClickedItem;

            Frame.Navigate(typeof(AccountBeheerEditPage), userId);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
