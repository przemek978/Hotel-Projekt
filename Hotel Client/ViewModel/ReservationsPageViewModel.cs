using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Hotel_Client.Models;
using Hotel_Client.Models.Util;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.View;
namespace Hotel_Client.ViewModel
{
    public class ReservationsPageViewModel : BaseViewModel
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IAlertService _alertService;
        private readonly IShareService _shareService;

        public ObservableCollection<Reservation> Reservations { get; set; } = new();

        public ReservationsPageViewModel(IHotelRepository hotelRepository, IAlertService alertService, IShareService shareService)
        {
            _hotelRepository = hotelRepository;
            _alertService = alertService;
            _shareService = shareService;
            GetReservations();
        }

        public async Task GetReservations()
        {
            try
            {
                var userId = App.UserId;
                var reservations = await _hotelRepository.GetReservations(userId);

                Reservations.Clear();
                if (reservations != null)
                {
                    foreach (var reservation in reservations)
                    {
                        Reservations.Add(reservation);
                    }
                }
            }
            catch (Exception e)
            {
                await _alertService.ShowAlertAsync(UIText.ErrorTitle, e.Message, UIText.CloseLabelButton);
            }
        }

        public async Task CancelReservation(Reservation reservation)
        {
            try
            {
                var userId = App.UserId;
                await _hotelRepository.CancelReservation(reservation.Number, userId);
                Reservations.Remove(reservation);
            }
            catch (Exception e)
            {
                await _alertService.ShowAlertAsync(UIText.ErrorTitle, e.Message, UIText.CloseLabelButton);
            }
        }

        public async Task ReservationSelection(Reservation selectedReservation)
        {
            await _shareService.Add("SelectedReservation", selectedReservation);
            var window = new ReservationDetailsPage(_hotelRepository, _alertService, _shareService);
            window.Show();
        }

        public async Task ShowConfirmation(Reservation selectedReservation)
        {
            var userId = App.UserId;
            var documentResponse = await _hotelRepository.GetConfirmationDocument(selectedReservation.Number, userId);
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "confirmation.pdf");
            await File.WriteAllBytesAsync(path, documentResponse);
            await _alertService.ShowAlertAsync(UIText.InfoTitle, string.Format(UIText.DocumentSaved, path), UIText.CloseLabelButton);
        }
    }
}
