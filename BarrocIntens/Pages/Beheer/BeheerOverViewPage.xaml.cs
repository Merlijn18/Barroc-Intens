using BarrocIntens.Pages.Financien;
using BarrocIntens.Pages.Inkoop;
using BarrocIntens.Pages.Inlog;
using BarrocIntens.Pages.Monteur;
using BarrocIntens.Pages.Sales;
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
    public sealed partial class BeheerOverViewPage : Page
    {
        public BeheerOverViewPage()
        {
            InitializeComponent();
        }



        private void MonteurOverView_Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MonteurOverViewPage));

        }

        private void FinancienOverView_Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FinancienOverViewPage));
        }

        private void SalesOverView_Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SalesOverViewPage));
        }

        private void AccountBeheerOverView_Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountBeheerOverViewPage));
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        private void InkoopOverView_Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InkoopOverViewPage));

        }
    }
}
