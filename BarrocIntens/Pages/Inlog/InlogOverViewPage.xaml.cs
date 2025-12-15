using BarrocIntens.Data;
using BarrocIntens.Pages.Beheer;
using BarrocIntens.Pages.Financien;
using BarrocIntens.Pages.Inkoop;
using BarrocIntens.Pages.Inlog;
using BarrocIntens.Pages.Maintenance;
using BarrocIntens.Pages.Sales;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.Pages.Inlog
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InlogOverViewPage : Page
    {
        public InlogOverViewPage()
        {
            InitializeComponent();
        }

        //Dev Inlog !Remove Before Publish!
        private void DevInlogButton_Click(object sender, RoutedEventArgs e)
        {
            //To Do: Password Hash
            var nameEmail = "Harry";
            var password = "123";


            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u => u.Username == nameEmail || u.Email == nameEmail);

            if (user != null && password != null)
            {
                //Checked User/Email,Paswoord
                if (password == user.Password && nameEmail == user.Username || nameEmail == user.Email)
                {
                    if (user.Role == "Beheer")
                    {
                        Frame.Navigate(typeof(BeheerOverViewPage), user.Id);
                    }
                    else
                    {
                        Frame.Navigate(typeof(InlogOverViewPage));
                    }
                }
                else
                {
                    MessageText.Text = "Wachtwoord of Gebruikersnaam/Email is niet geldig!";
                }
            }
            else
            {
                MessageText.Text = "Email/Gebruikersnaam of Wachtwoord is niet Ingevuld!";
            }
        }

        //Inlog Users Account
        private void InlogButton_Click(object sender, RoutedEventArgs e)
        {
            //To Do: Password Hash
            var nameEmail = NameEmailTextBox.Text.Trim();
            var password = PasswordTextBox.Password.Trim();


            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u => u.Username == nameEmail || u.Email == nameEmail);

            if (user != null && password != null)
            {
                //Checked User/Email,Paswoord
                if (password == user.Password && nameEmail == user.Username || nameEmail == user.Email)
                {
                    if (user.Role == "Beheer")
                    {
                        Frame.Navigate(typeof(BeheerOverViewPage), user.Id);
                    }
                    else if (user.Role == "Sales")
                    {
                        Frame.Navigate(typeof(SalesOverViewPage), user.Id);
                    }
                    else if(user.Role == "Inkoop")
                    {
                        Frame.Navigate(typeof(InkoopOverViewPage), user.Id);
                    }
                    else if(user.Role == "Financien")
                    {
                        Frame.Navigate(typeof(FinancienOverViewPage), user.Id);
                    }
                    else if(user.Role == "Monteur")
                    {
                        Frame.Navigate(typeof(MaintenanceOverviewPage), user.Id);
                    }
                    else
                    {
                        Frame.Navigate(typeof(InlogOverViewPage));
                    }
                }
                else
                {
                    MessageText.Text = "Wachtwoord of Gebruikersnaam/Email is niet geldig!";
                }
            }
            else
            {
                MessageText.Text = "Email/Gebruikersnaam of Wachtwoord is niet Ingevuld!";
            }
        }


    }
}
