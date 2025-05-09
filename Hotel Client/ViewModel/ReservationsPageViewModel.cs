using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Hotel_Client.Models;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.Command;

namespace Hotel_Client.ViewModel
{
    public class ReservationsPageViewModel : BaseViewModel
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IAlertService _alertService;
        private readonly IShareService _shareService;

        public ObservableCollection<Reservation> Reservations { get; set; } = new();

        public ICommand GetReservationsCommand { get; }
        public ICommand CancelReservationCommand { get; }
        public ICommand ReservationSelectionCommand { get; }
        public ICommand ShowConfirmationCommand { get; }
        public ICommand BackCommand { get; }

        public ReservationsPageViewModel(IHotelRepository hotelRepository, IAlertService alertService, IShareService shareService)
        {
            _hotelRepository = hotelRepository;
            _alertService = alertService;
            _shareService = shareService;

            GetReservationsCommand = new RelayCommand(async () => await GetReservations());
            CancelReservationCommand = new RelayCommand<Reservation>(async reservation => await CancelReservation(reservation));
            ReservationSelectionCommand = new RelayCommand<Reservation>(async reservation => await ReservationSelection(reservation));
            ShowConfirmationCommand = new RelayCommand<Reservation>(async reservation => await ShowConfirmation(reservation));
            BackCommand = new RelayCommand(Back);
        }

        private async Task GetReservations()
        {
            try
            {
                var userId = Properties.Settings.Default.UserID;
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
                await _alertService.ShowAlertAsync("Error", e.Message, "Exit");
            }
        }

        private async Task CancelReservation(Reservation reservation)
        {
            try
            {
                var userId = Properties.Settings.Default.UserID;
                await _hotelRepository.CancelReservation(reservation.Number, userId);
                Reservations.Remove(reservation);
            }
            catch (Exception e)
            {
                await _alertService.ShowAlertAsync("Error", e.Message, "Exit");
            }
        }

        private async Task ReservationSelection(Reservation selectedReservation)
        {
            await _shareService.Add("SelectedReservation", selectedReservation);
            // Add navigation to ReservationDetailsPage if using navigation framework
        }

        private async Task ShowConfirmation(Reservation selectedReservation)
        {
            var userId = Properties.Settings.Default.UserID;
            var documentResponse = await _hotelRepository.GetConfirmationDocument(selectedReservation.Number, userId);
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "confirmation.pdf");
            await File.WriteAllBytesAsync(path, documentResponse);
            await _alertService.ShowAlertAsync("Info", "Successfully saved file in Documents folder!", "Exit");
        }

        private void Back()
        {
            // Implement navigation logic here
        }
    }
}
