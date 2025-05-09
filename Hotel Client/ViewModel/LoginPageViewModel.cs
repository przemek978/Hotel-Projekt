using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.Command;
using Hotel_Client.Services;

namespace Hotel_Client.ViewModel
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IAlertService _alertService;

        private string _username = "test";
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        private string _password = "latwehaslo";
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public ICommand LoginCommand { get; }

        public LoginPageViewModel(IHotelRepository hotelRepository, IAlertService alertService)
        {
            _hotelRepository = hotelRepository;
            _alertService = alertService;
            LoginCommand = new RelayCommand(async () => await Login());
        }

        private async Task Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return;

            try
            {
                var id = await _hotelRepository.Login(Username, Password);

                if (id != 0)
                {
                    //Properties.Settings.Default.UserID = id;
                    //Properties.Settings.Default.UserName = Username;
                    //Properties.Settings.Default.PassWord = Password;
                    //Properties.Settings.Default.Save();

                    Application.Current.MainWindow.DataContext = new HomePageViewModel(_hotelRepository, _alertService, new ShareService());
                }
            }
            catch (Exception e)
            {
                await _alertService.ShowAlertAsync("Error", e.Message, "Cancel");
            }
        }
    }
}