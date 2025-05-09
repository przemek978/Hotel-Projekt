using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel_Client.Models;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.Command;
using System.Windows;

namespace Hotel_Client.ViewModel
{
    public class MakeReservationPageViewModel : BaseViewModel
    {
        private readonly IShareService _shareService;
        private readonly IAlertService _alertService;
        private readonly IHotelRepository _hotelRepository;

        private DateTime _dateFrom = DateTime.Today;
        public DateTime DateFrom
        {
            get => _dateFrom;
            set { _dateFrom = value; OnPropertyChanged(nameof(DateFrom)); }
        }

        private DateTime _dateTo = DateTime.Today.AddDays(2);
        public DateTime DateTo
        {
            get => _dateTo;
            set { _dateTo = value; OnPropertyChanged(nameof(DateTo)); }
        }

        private string _note = string.Empty;
        public string Note
        {
            get => _note;
            set { _note = value; OnPropertyChanged(nameof(Note)); }
        }

        public ObservableCollection<Room> BookedRooms { get; set; } = new();

        public ICommand GetBookedRoomsCommand { get; }
        public ICommand RemoveRoomCommand { get; }
        public ICommand ConfirmReservationCommand { get; }
        public ICommand BackCommand { get; }

        public MakeReservationPageViewModel(IShareService shareService, IAlertService alertService, IHotelRepository hotelRepository)
        {
            _shareService = shareService;
            _alertService = alertService;
            _hotelRepository = hotelRepository;

            GetBookedRoomsCommand = new RelayCommand(async () => await GetBookedRooms());
            RemoveRoomCommand = new RelayCommand<Room>(async room => await RemoveRoom(room));
            ConfirmReservationCommand = new RelayCommand(async () => await ConfirmReservation());
            BackCommand = new RelayCommand(Back);
        }

        private async Task GetBookedRooms()
        {
            BookedRooms.Clear();
            var rooms = await _shareService.GetBookedRooms();

            if (rooms.Count != 0)
            {
                foreach (var room in rooms)
                {
                    BookedRooms.Add(room);
                }
            }
            else
            {
                await _alertService.ShowAlertAsync("Error", "Please select at least one room!", "Close");
            }
        }

        private async Task RemoveRoom(Room selectedRoom)
        {
            BookedRooms.Remove(selectedRoom);
            await _shareService.RemoveRoom<Room>(selectedRoom);
        }

        private async Task ConfirmReservation()
        {
            try
            {
                var userId = Properties.Settings.Default.UserID;
                var bookedRoomsNumbers = BookedRooms.Select(r => r.RoomNumber).ToList();
                var reservationNumber = await _hotelRepository.MakeReservation(bookedRoomsNumbers, DateFrom, DateTo, Note, userId);

                await _alertService.ShowAlertAsync("Confirmation", $"Your reservation number: {reservationNumber}", "Close");

                BookedRooms.Clear();
                Note = string.Empty;
            }
            catch (Exception e)
            {
                await _alertService.ShowAlertAsync("Error", e.Message, "Exit");
            }
        }

        private void Back()
        {
            Application.Current.MainWindow.DataContext = new HomePageViewModel(_hotelRepository, _alertService, _shareService);
        }
    }
}
