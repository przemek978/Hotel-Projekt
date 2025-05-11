using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Hotel_Client.Models;
using Hotel_Client.Models.Util;
using Hotel_Client.Repositories;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.View;

namespace Hotel_Client.ViewModel
{
    public class HomePageViewModel : BaseViewModel
    {
        public readonly IHotelRepository _hotelRepository;
        public readonly IAlertService _alertService;
        public readonly IShareService _shareService;
        public Action? CloseAction { get; set; }

        public DateTime DateFrom { get; set; } = DateTime.Today;
        public DateTime DateTo { get; set; } = DateTime.Today.AddDays(2);

        private ObservableCollection<Room> _rooms = new ObservableCollection<Room>();
        public ObservableCollection<Room> Rooms
        {
            get => _rooms;
            set { _rooms = value; OnPropertyChanged(nameof(Rooms)); }
        }

        public HomePageViewModel(IHotelRepository hotelRepository, IAlertService alertService, IShareService shareService)
        {
            _hotelRepository = hotelRepository;
            _alertService = alertService;
            _shareService = shareService;
        }

        public async Task GetRooms()
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
                await _alertService.ShowAlertAsync(UIText.ErrorTitle, e.Message, UIText.CancelLabelButton);
            }
        }

        public async Task RoomSelection(Room selectedRoom)
        {
            await _shareService.Add("SelectedRoom", selectedRoom);
            var window = new RoomDetailsPage(_hotelRepository, _alertService, _shareService);
            window.Show();
        }

        public async Task AddRoom(Room selectedRoom)
        {
            await _shareService.AddRoom<Room>(selectedRoom.RoomNumber, selectedRoom);
            await _alertService.ShowAlertAsync(UIText.ConfirmTitle, UIText.AddRoomConfirmMessage, UIText.CloseLabelButton);

            Rooms.Remove(selectedRoom);
        }

        public async void MakeReservation()
        {
            var rooms = await _shareService.GetBookedRooms();

            if (rooms.Count > 0)
            {
                var window = new MakeReservationPage(_hotelRepository, _alertService, _shareService, DateFrom, DateTo);
                window.Show();
            }
            else
            {
                await _alertService.ShowAlertAsync(UIText.ErrorTitle, UIText.SelectLeastOneRoomMessage, UIText.CloseLabelButton);
            }
        }

        public void GetReservations()
        {

            var window = new ReservationsPage(_hotelRepository, _alertService, _shareService);
            window.Show();
        }

        public void Logout()
        {
            App.UserId = 0;
            App.Username = string.Empty;
            var window = new LoginPage(_hotelRepository, _alertService, _shareService);
            window.Show();
            CloseAction?.Invoke();
        }
    }
}