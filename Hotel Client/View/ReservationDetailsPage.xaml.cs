using Hotel_Client.Models;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hotel_Client.View
{
    public partial class ReservationDetailsPage : Window
    {
        private ReservationDetailsPageViewModel _viewModel;
        public ReservationDetailsPage(IHotelRepository hotelRepository, IAlertService alertService, IShareService shareService)
        {
            InitializeComponent();
            _viewModel = new ReservationDetailsPageViewModel(hotelRepository, alertService, shareService);
            DataContext = _viewModel;
        }

        private async void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.SaveChanges();
        }

        private async void RemoveRoom_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Room room)
            {
                await _viewModel.RemoveRoom(room);
            }
        }

    }
}
