using Hotel_Client.Models;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hotel_Client.View
{
    public partial class ReservationsPage : Window
    {
        private readonly ReservationsPageViewModel _viewModel;

        public ReservationsPage(IHotelRepository hotelRepository, IAlertService alertService, IShareService shareService)
        {
            InitializeComponent();
            _viewModel = new ReservationsPageViewModel(hotelRepository, alertService, shareService);
            DataContext = _viewModel;
        }

        private async void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Reservation reservation)
                await _viewModel.CancelReservation(reservation);
        }

        private async void ShowConfirmation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Reservation reservation)
                await _viewModel.ShowConfirmation(reservation);
        }

        private async void ShowDetails_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Reservation reservation)
                await _viewModel.ReservationSelection(reservation);
        }
    }
}
