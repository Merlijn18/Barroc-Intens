using BarrocIntens.Data;
using BarrocIntens.Models;
using BarrocIntens.Pages.Inlog;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BarrocIntens.Pages.Sales
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double d) return $"€{d:N2}";
            if (value is decimal dec) return $"€{dec:N2}";
            return value?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => null;
    }

    public sealed partial class SalesOverViewPage : Page
    {
        private ObservableCollection<Offer> Offers = new ObservableCollection<Offer>();
        private Offer SelectedOffer;

        // Notes functionaliteit
        private bool _notesOpen = false;

        public SalesOverViewPage()
        {
            this.InitializeComponent();

            // Zet de ObservableCollection als ItemsSource voor de ListView
            OfferListView.ItemsSource = Offers;

            // Laad data uit database
            LoadOffersFromDatabase();

            // Laad opgeslagen notities
            LoadNotes();
        }

        #region Database & Offers

        private async void LoadOffersFromDatabase()
        {
            using var db = new AppDbContext();
            var offersFromDb = await db.Offers
                .Include(o => o.Customer)
                .Include(o => o.Items)
                .ToListAsync();

            Offers.Clear();
            foreach (var offer in offersFromDb)
                Offers.Add(offer);
        }

        private async Task LoadAllOffers()
        {
            using var db = new AppDbContext();
            var allOffers = await db.Offers
                .Include(o => o.Customer)
                .Include(o => o.Items)
                .ToListAsync();

            Offers.Clear();
            foreach (var offer in allOffers)
                Offers.Add(offer);
        }

        private async Task LoadOffersByStatus(OfferStatus status)
        {
            using var db = new AppDbContext();
            var filteredOffers = await db.Offers
                .Include(o => o.Customer)
                .Include(o => o.Items)
                .Where(o => o.Status == status)
                .ToListAsync();

            Offers.Clear();
            foreach (var offer in filteredOffers)
                Offers.Add(offer);
        }

        private string GenerateOfferNumber() => $"OFF-{DateTime.Now:yyyyMMddHHmmss}";
        private string GenerateContractNumber() => $"CN-{DateTime.Now:yyyyMMddHHmmss}";
        private string GenerateCustomerNumber() => $"CUST-{DateTime.Now:yyyyMMddHHmmss}";

        #endregion

        #region Buttons & Navigation

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            User.LoggedInUser = null;
            Frame.Navigate(typeof(InlogOverViewPage));
        }

        private void CreateOffer_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OfferCreatePage));
        }

        private void EditOffer_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Offer offer)
                Frame.Navigate(typeof(OfferEditPage), offer.Id);
        }

        private async void FilterConcept_Click(object sender, RoutedEventArgs e) =>
            await LoadOffersByStatus(OfferStatus.Concept);

        private async void FilterVerstuurd_Click(object sender, RoutedEventArgs e) =>
            await LoadOffersByStatus(OfferStatus.Verstuurd);

        private async void FilterAll_Click(object sender, RoutedEventArgs e) =>
            await LoadAllOffers();

        private void OfferListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Offer selectedOffer)
                Frame.Navigate(typeof(OfferDetailsPage), selectedOffer);
        }

        #endregion

        #region Notes / Kladblok

        private void ToggleNotes_Click(object sender, RoutedEventArgs e)
        {
            _notesOpen = !_notesOpen;

            VisualStateManager.GoToState(this,
                _notesOpen ? "NotesOpen" : "NotesClosed",
                true
            );
        }

        private async void SaveNotes_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();
            var note = await db.Notes.FirstOrDefaultAsync();

            if (note == null)
            {
                note = new Notes
                {
                    Note = NotesTextBox.Text,
                    CreatedAt = DateTime.Now
                };
                db.Notes.Add(note);
            }
            else
            {
                note.Note = NotesTextBox.Text;
            }

            await db.SaveChangesAsync();

            var dialog = new ContentDialog
            {
                Title = "Opgeslagen",
                Content = "Notities zijn opgeslagen.",
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }
        

        private async void LoadNotes()
        {
            using var db = new AppDbContext();
            var note = await db.Notes.FirstOrDefaultAsync();
            if (note != null)
            {
                NotesTextBox.Text = note.Note;
            }
        }

        #endregion
    }
}
 