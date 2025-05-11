using Hotel_Client.Models;
using Hotel_Client.Repositories;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hotel_Client.View
{
    public partial class MakeReservationPage : Window
    {
        private MakeReservationPageViewModel _viewModel;

        public MakeReservationPage(IHotelRepository hotelRepository,
            IAlertService alertService,
            IShareService shareService,
            DateTime dateFrom,
            DateTime dateTo
            )
        {
            InitializeComponent();
            _viewModel = new MakeReservationPageViewModel(hotelRepository, alertService, shareService, dateFrom, dateTo);
            DataContext = _viewModel;
        }

        private async void ConfirmReservation_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.ConfirmReservation();
        }

        private async void RemoveRoom_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Room room)
            {
                await _viewModel.RemoveRoom(room);
            }
        }

    }
}
