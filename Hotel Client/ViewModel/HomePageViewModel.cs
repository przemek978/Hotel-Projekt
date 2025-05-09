using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Hotel_Client.Command;
using Hotel_Client.Models;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services;
using Hotel_Client.Services.Interfaces;

namespace Hotel_Client.ViewModel
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IAlertService _alertService;
        private readonly IShareService _shareService;

        public DateTime DateFrom { get; set; } = DateTime.Today;
        public DateTime DateTo { get; set; } = DateTime.Today.AddDays(2);

        private ObservableCollection<Room> _rooms = new ObservableCollection<Room>();
        public ObservableCollection<Room> Rooms
        {
            get => _rooms;
            set { _rooms = value; OnPropertyChanged(nameof(Rooms)); }
        }

        public ICommand GetRoomsCommand { get; }
        public ICommand GetReservationsCommand { get; }
        public ICommand MakeReservationCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand RoomSelectionCommand { get; }
        public ICommand AddRoomCommand { get; }

        public HomePageViewModel(IHotelRepository hotelRepository, IAlertService alertService, IShareService shareService)
        {
            _hotelRepository = hotelRepository;
            _alertService = alertService;
            _shareService = shareService;

            GetRoomsCommand = new RelayCommand(async () => await GetRooms());
            GetReservationsCommand = new RelayCommand(GetReservations);
            MakeReservationCommand = new RelayCommand(MakeReservation);
            LogoutCommand = new RelayCommand(Logout);
            RoomSelectionCommand = new RelayCommand<Room>(async room => await RoomSelection(room));
            AddRoomCommand = new RelayCommand<Room>(async room => await AddRoom(room));
        }

        private async Task GetRooms()
        {
            try
            {
                Rooms.Clear();
                var rooms = await _hotelRepository.GetRooms(DateFrom, DateTo);
                if (rooms != null)
                {
                    foreach (var room in rooms)
                    {
                        Rooms.Add(room);
                    }
                }
            }
            catch (Exception e)
            {
                await _alertService.ShowAlertAsync("Error", e.Message, "Cancel");
            }
        }

        private void GetReservations()
        {
            Application.Current.MainWindow.DataContext = new ReservationsPageViewModel(_hotelRepository, _alertService, _shareService);
        }

        private async Task RoomSelection(Room selectedRoom)
        {
            await _shareService.Add("SelectedRoom", selectedRoom);
            Application.Current.MainWindow.DataContext = new RoomDetailsPageViewModel(_shareService, _alertService);
        }

        private async Task AddRoom(Room selectedRoom)
        {
            await _shareService.AddRoom<Room>(selectedRoom.RoomNumber, selectedRoom);
            await _alertService.ShowAlertAsync("Confirmation", "Added room to reservation!", "Exit");
            Rooms.Remove(selectedRoom);
        }

        private void MakeReservation()
        {
            Application.Current.MainWindow.DataContext = new MakeReservationPageViewModel(_shareService, _alertService, _hotelRepository);
        }

        private void Logout()
        {
            //Properties.Settings.Default.UserID = 0;
            //Properties.Settings.Default.UserName = string.Empty;
            //Properties.Settings.Default.PassWord = string.Empty;
            //Properties.Settings.Default.Save();

            Application.Current.MainWindow.DataContext = new LoginPageViewModel(_hotelRepository, _alertService);
        }
    }
}