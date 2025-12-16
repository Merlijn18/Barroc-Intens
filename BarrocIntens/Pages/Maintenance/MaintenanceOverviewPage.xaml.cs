using BarrocIntens.Pages.Financien;
using BarrocIntens.Pages.Inlog;
using BarrocIntens.Models;
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
    public sealed partial class MaintenanceOverviewPage : Page
    {
        public MaintenanceOverviewPage()
        {
            InitializeComponent();
        }

        private void Calendar_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MaintenanceCalendarPage));
        }

        private void Machines_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MaintenanceMachinesPage));
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MaintenanceReportPage));
        }

        private void BackLog_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MaintenanceBackLog));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            User.LoggedInUser = null;
            Frame.Navigate(typeof(InlogOverViewPage));
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

    }
}
