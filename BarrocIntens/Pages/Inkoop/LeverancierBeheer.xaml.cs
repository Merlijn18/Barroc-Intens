using BarrocIntens.Data;
using BarrocIntens.Pages.Inkoop;
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

namespace BarrocIntens.Pages.Inkoop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LeverancierBeheer : Page
    {
        public LeverancierBeheer()
        {
            InitializeComponent();
            LoadLeveranciers();
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        // Back knop
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void LoadLeveranciers()
        {
            using var db = new AppDbContext();

            var leveranciers = db.Leveranciers.ToList();
           
            LeverancierListView.ItemsSource = leveranciers;
        }

        private void LeverancierAanmaken_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LeverancierAanmaken));
        }
    }
}
