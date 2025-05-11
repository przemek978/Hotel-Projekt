using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Hotel_Client.Models;
using Hotel_Client.Models.Util;
using Hotel_Client.Services.Interfaces;
namespace Hotel_Client.ViewModel
{
    public class RoomDetailsPageViewModel : BaseViewModel
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

        public RoomDetailsPageViewModel(IAlertService alertService, IShareService shareService)
        {
            _shareService = shareService;
            _alertService = alertService;
            GetRoomDetails();
        }

        public async Task AddRoomReservation()
        {
            await _shareService.AddRoom<Room>(Room.RoomNumber, Room);
            await _alertService.ShowAlertAsync(UIText.ConfirmTitle, UIText.AddRoomConfirmMessage, UIText.CloseLabelButton);
        }

        private async Task GetRoomDetails()
        {
            var roomTask = _shareService.GetValue<Room>("SelectedRoom");
            var room = await roomTask;
            if (room == null) return;

            Room = room;
            HasDoubleBed = Room.HasDoubleBed ? "Tak" : "Nie";
        }
    }
}