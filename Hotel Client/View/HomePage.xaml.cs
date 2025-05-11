using Hotel_Client.Models;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hotel_Client.View
{
    public partial class HomePage : Window
    {
        private HomePageViewModel _viewModel;

        public HomePage(IHotelRepository hotelRepository, IAlertService alertService, IShareService shareService)
        {
            InitializeComponent();
            _viewModel = new HomePageViewModel(hotelRepository, alertService, shareService);
            _viewModel.CloseAction = () => this.Close();
            DataContext = _viewModel;
        }

        private void MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MakeReservation();
        }

        private void GetReservations_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.GetReservations();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Logout();
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.GetRooms();
        }

        private async void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var room = button.Tag as Room;

            if (room != null)
            {
                await _viewModel.AddRoom(room);
            }
        }

        private async void RoomBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Room room)
            {
                await _viewModel.RoomSelection(room); 
            }
        }
    }
}
