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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.Pages.Inkoop
{
    public sealed partial class InkoopOverViewPage : Page
    {
        public InkoopOverViewPage()
        {
            InitializeComponent();
            LoadProduct();

        }
        private void LoadProduct()
        {
            using var db = new AppDbContext();

            var product = db.Products
                .ToList();

            ProductListView.ItemsSource = product;

        }


        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void productSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchQuery = productSearchTextBox.Text;

            using var db = new AppDbContext();

            ProductListView.ItemsSource = db.Products
                .Where(p => p.Productname.Contains(searchQuery) || p.Stock.ToString().Contains(searchQuery))
                .OrderByDescending(p => p.Productname)
                .ToList();


        }
    }
}

