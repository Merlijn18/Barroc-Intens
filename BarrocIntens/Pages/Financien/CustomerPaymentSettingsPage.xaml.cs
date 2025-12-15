using BarrocIntens.Data;
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

namespace BarrocIntens.Pages.Financien
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerPaymentSettingsPage : Page
    {
        public CustomerPaymentSettingsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using var context = new AppDbContext();
            CustomerDropdown.ItemsSource = context.Customers.ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDropdown.SelectedValue == null || TermSelector.SelectedValue == null) return;

            using var context = new AppDbContext();
            int id = (int)CustomerDropdown.SelectedValue;
            int term = int.Parse((TermSelector.SelectedItem as ComboBoxItem).Content.ToString());

            var customer = context.Customers.Find(id);
            customer.PaymentTermDays = term;
            context.SaveChanges();
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
