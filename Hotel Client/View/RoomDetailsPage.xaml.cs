using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel_Client.View
{
    public partial class RoomDetailsPage : Window
    {
        private RoomDetailsPageViewModel _viewModel;
        public RoomDetailsPage(IHotelRepository hotelRepository, IAlertService alertService, IShareService shareService)
        {
            InitializeComponent();
            _viewModel = new RoomDetailsPageViewModel(alertService,shareService);
            DataContext = _viewModel;
        }

        private async void AddRoomReservation_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.AddRoomReservation();
        }
    }
}
