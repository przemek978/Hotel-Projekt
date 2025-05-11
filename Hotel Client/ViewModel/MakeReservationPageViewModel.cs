using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel_Client.Models;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services.Interfaces;
using System.Windows;
using Hotel_Client.View;
using Hotel_Client.Models.Util;

namespace Hotel_Client.ViewModel
{
    public class MakeReservationPageViewModel : BaseViewModel
    {
        private readonly IShareService _shareService;
        private readonly IAlertService _alertService;
        private readonly IHotelRepository _hotelRepository;
        public Action? CloseAction { get; set; }

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

        public MakeReservationPageViewModel(IHotelRepository hotelRepository,
            IAlertService alertService,
            IShareService shareService,
            DateTime dateFrom,
            DateTime dateTo
        )
        {
            _shareService = shareService;
            _alertService = alertService;
            _hotelRepository = hotelRepository;
            _dateFrom = dateFrom;
            _dateTo = dateTo;
            GetBookedRooms();
        }

        public async Task GetBookedRooms()
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
                await _alertService.ShowAlertAsync(UIText.ErrorTitle, UIText.SelectLeastOneRoomMessage, UIText.CloseLabelButton);
            }
        }

        public async Task RemoveRoom(Room selectedRoom)
        {
            BookedRooms.Remove(selectedRoom);
            await _shareService.RemoveRoom<Room>(selectedRoom);
        }

        public async Task ConfirmReservation()
        {
            try
            {
                var userId = App.UserId;
                var bookedRoomsNumbers = BookedRooms.Select(r => r.RoomNumber).ToList();
                var reservationNumber = await _hotelRepository.MakeReservation(bookedRoomsNumbers, DateFrom, DateTo, Note, userId);

                await _alertService.ShowAlertAsync(UIText.ConfirmTitle, string.Format(UIText.ReservationNumberFormatMessage, reservationNumber), UIText.CloseLabelButton);

                BookedRooms.Clear();
                Note = string.Empty;
            }
            catch (Exception e)
            {
                await _alertService.ShowAlertAsync(UIText.ErrorTitle, e.Message, UIText.CloseLabelButton);
            }
        }

        public void Back()
        {
            Application.Current.MainWindow.DataContext = new HomePageViewModel(_hotelRepository, _alertService, _shareService);
        }
    }
}
