using BarrocIntens.Data;
using BarrocIntens.Models;
using BarrocIntens.Pages.Inlog;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;

namespace BarrocIntens.Pages.Maintenance
{

    public sealed partial class MaintenanceCalendarPage : Page
    {
        private List<AgendaKlus> Klussen;

        public MaintenanceCalendarPage()
        {
            InitializeComponent();

            using var db = new AppDbContext();
            Klussen = db.AgendaKlusses.ToList();
            LoadToDo();
        }

        private void LoadToDo()
        {
            using var db = new AppDbContext();

            var toDo = db.AgendaKlusses
                .ToList();

            ToDoListView.ItemsSource = toDo;
        }

        private void LoadCalander()
        {

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();

        }

        private async void MaintenanceCalendar_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            if (args.AddedDates.Count == 0)
                return;

            DateTime date = args.AddedDates[0].Date;

            using var db = new AppDbContext();
            var klussenVandaag = db.AgendaKlusses
                .Where(k => k.Date.Date == date.Date)
                .ToList();

            string overzicht = klussenVandaag.Count == 0
                ? "Geen klussen op deze dag."
                : string.Join("\n", klussenVandaag.Select(k =>
                    $"• {k.Type} – {k.Titel} ({k.ExtraInfo})"));

            ContentDialog dialog = new()
            {
                Title = date.ToString("dd MMMM yyyy"),
                Content = overzicht,
                PrimaryButtonText = "Keuring toevoegen",
                SecondaryButtonText = "Melding toevoegen",
                CloseButtonText = "Sluiten",
                XamlRoot = this.Content.XamlRoot
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
                Frame.Navigate(typeof(MaintenanceInspectionPage), date);

            else if (result == ContentDialogResult.Secondary)
                Frame.Navigate(typeof(MaintenanceReportPage), date);
        }

        private void ResetDag(CalendarViewDayItem item)
        {
            item.ClearValue(CalendarViewDayItem.BackgroundProperty);
            item.ClearValue(CalendarViewDayItem.BorderBrushProperty);
            item.ClearValue(CalendarViewDayItem.BorderThicknessProperty);
        }


        private void MarkeerDagKeuring(CalendarViewDayItem item)
        {
            item.Background = new SolidColorBrush(Colors.LawnGreen);
            item.BorderBrush = new SolidColorBrush(Colors.DarkGreen);
            item.BorderThickness = new Thickness(2);
        }

        private void MarkeerDagMelding(CalendarViewDayItem item)
        {
            item.Background = new SolidColorBrush(Colors.IndianRed);
            item.BorderBrush = new SolidColorBrush(Colors.DarkRed);
            item.BorderThickness = new Thickness(2);
        }

        private void MarkeerDagKeuringEnMelding(CalendarViewDayItem item)
        {
            item.Background = new SolidColorBrush(Colors.SkyBlue);
            item.BorderBrush = new SolidColorBrush(Colors.DarkBlue);
            item.BorderThickness = new Thickness(2);
        }


        private void MaintenanceCalendar_DayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            var date = args.Item.Date.Date;

            // Alle klussen op deze specifieke dag
            var klussenOpDag = Klussen.Where(k => k.Date.Date == date).ToList();

            if (klussenOpDag.Count == 0)
            {
                ResetDag(args.Item);
                return;
            }

            bool heeftKeuring = klussenOpDag.Any(k => k.Type == "Keuring");
            bool heeftMelding = klussenOpDag.Any(k => k.Type == "Melding");

            if (heeftKeuring && heeftMelding)
            {
                MarkeerDagKeuringEnMelding(args.Item);
            }
            else if (heeftKeuring)
            {
                MarkeerDagKeuring(args.Item);
            }
            else if (heeftMelding)
            {
                MarkeerDagMelding(args.Item);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var button = sender as Button;
            var ToDoId = button?.DataContext as AgendaKlus;

            if (ToDoId != null)
            {
                //Show Pop-Up Message
                var confirmDialog = new ContentDialog
                {
                    Title = "Weet je het zeker?",
                    Content = "Weet je zeker dat je deze Taak wilt verwijderen?",
                    PrimaryButtonText = "Ja",
                    CloseButtonText = "Nee",
                    XamlRoot = this.XamlRoot
                };

                var result = await confirmDialog.ShowAsync();

                //If oke
                if (result == ContentDialogResult.Primary)
                {
                    db.AgendaKlusses.Remove(ToDoId);
                    db.SaveChanges();
                    LoadToDo();
                    var dialog = new ContentDialog
                    {
                        Title = "Verwijderd!",
                        Content = "Taak is verwijderd.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };

                    await dialog.ShowAsync();
                }
            }
        }

        private void ToDoListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void ToDoSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchQuery = ToDoSearchTextBox.Text;

            using var db = new AppDbContext();

            ToDoListView.ItemsSource = db.AgendaKlusses
                .Where(c => c.Type.Contains(searchQuery) || c.Titel.Contains(searchQuery))
                .OrderByDescending(c => c.Date)
                .ToList();
        }
    }
}
