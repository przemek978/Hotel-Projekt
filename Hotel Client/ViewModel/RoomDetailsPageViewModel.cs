using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Hotel_Client.Models;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.Command;

namespace Hotel_Client.ViewModel
{
    public class RoomDetailsPageViewModel : INotifyPropertyChanged
    {
        private readonly IShareService _shareService;
        private readonly IAlertService _alertService;

        private Room _room = new();
        public Room Room
        {
            get => _room;
            set { _room = value; OnPropertyChanged(); }
        }

        private string _hasDoubleBed;
        public string HasDoubleBed
        {
            get => _hasDoubleBed;
            set { _hasDoubleBed = value; OnPropertyChanged(); }
        }

        private string _hasBathroom;
        public string HasBathroom
        {
            get => _hasBathroom;
            set { _hasBathroom = value; OnPropertyChanged(); }
        }

        private string _isPresidentialSuite;
        public string IsPresidentialSuite
        {
            get => _isPresidentialSuite;
            set { _isPresidentialSuite = value; OnPropertyChanged(); }
        }

        public ICommand GetRoomDetailsCommand { get; }
        public ICommand AddRoomReservationCommand { get; }
        public ICommand BackCommand { get; }

        public RoomDetailsPageViewModel(IShareService shareService, IAlertService alertService)
        {
            _shareService = shareService;
            _alertService = alertService;

            GetRoomDetailsCommand = new RelayCommand(async _ => await GetRoomDetails());
            AddRoomReservationCommand = new RelayCommand(async _ => await AddRoomReservation());
            BackCommand = new RelayCommand(_ => Back());
        }

        private async Task GetRoomDetails()
        {
            var roomTask = _shareService.GetValue<Room>("SelectedRoom");
            var room = await roomTask;
            if (room == null) return;

            Room = room;
            HasDoubleBed = Room.HasDoubleBed ? "Yes" : "No";
            HasBathroom = Room.HasBathroom ? "Yes" : "No";
            IsPresidentialSuite = Room.IsPresidentialSuite ? "Yes" : "No";
        }

        private async Task AddRoomReservation()
        {
            await _shareService.AddRoom(Room.RoomNumber, Room);
            await _alertService.ShowAlertAsync("Confirmation", "Added room to reservation!", "Close");
        }

        private void Back()
        {
            // implement navigation logic (e.g. frame navigation) here
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}