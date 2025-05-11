using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel_Client.Models;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services.Interfaces;
namespace Hotel_Client.ViewModel
{
    public class ReservationDetailsPageViewModel : BaseViewModel
    {
        private readonly IShareService _shareService;
        private readonly IHotelRepository _hotelRepository;
        private readonly IAlertService _alertService;

        private Reservation _reservation;
        public Reservation Reservation
        {
            get => _reservation;
            set => SetProperty(ref _reservation, value);
        }

        private DateTime _dateFrom;
        public DateTime DateFrom
        {
            get => _dateFrom;
            set => SetProperty(ref _dateFrom, value);
        }

        private DateTime _dateTo;
        public DateTime DateTo
        {
            get => _dateTo;
            set => SetProperty(ref _dateTo, value);
        }

        private string _note = string.Empty;
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        public ObservableCollection<Room> ReservationRooms { get; set; } = new();

        public ReservationDetailsPageViewModel(IHotelRepository hotelRepository, IAlertService alertService, IShareService shareService)
        {
            _shareService = shareService;
            _hotelRepository = hotelRepository;
            _alertService = alertService;
            GetReservationDetails();
        }

        public Task RemoveRoom(Room selectedRoom)
        {
            ReservationRooms.Remove(selectedRoom);
            return Task.CompletedTask;
        }

        public async Task SaveChanges()
        {
            try
            {
                var userId = App.UserId;
                var newReservation = new Reservation
                {
                    Number = Reservation.Number,
                    From = DateFrom,
                    To = DateTo,
                    Rooms = new List<Room>(ReservationRooms),
                    OwnersId = Reservation.OwnersId,
                    Notes = Note
                };

                await _hotelRepository.ModifyReservation(newReservation, userId);
                await _alertService.ShowAlertAsync("Confirmation", "Saved changes!", "Close");
            }
            catch (Exception e)
            {
                await _alertService.ShowAlertAsync("Error", e.Message, "Close");
            }
        }

        private async Task GetReservationDetails()
        {
            var reservation = await _shareService.GetValue<Reservation>("SelectedReservation");
            if (reservation == null) return;

            Reservation = reservation;
            ReservationRooms.Clear();

            foreach (var room in reservation.Rooms)
            {
                ReservationRooms.Add(room);
            }

            DateFrom = reservation.From;
            DateTo = reservation.To;
            Note = reservation.Notes;
        }
    }
}