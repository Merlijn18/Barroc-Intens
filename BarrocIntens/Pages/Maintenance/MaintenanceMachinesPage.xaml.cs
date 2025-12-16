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

namespace BarrocIntens.Pages.Maintenance
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MaintenanceMachinesPage : Page
    {
        public MaintenanceMachinesPage()
        {
            InitializeComponent();
            LoadMachines();
        }

        private void LoadMachines()
        {
            using var db = new AppDbContext();

            MachinesList.ItemsSource = db.Machines
                .ToList();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchQueary = SearchBox.Text;

            using var db = new AppDbContext();

            MachinesList.ItemsSource = db.Machines
                .Where(m => m.Name.Contains(searchQueary) || m.ArticleNumber.Contains(searchQueary))
                .OrderByDescending(c => c.LastMaintenaceDate)
                .ToList();
        }

        private void Detail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            User.LoggedInUser = null;
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        
    }
}
