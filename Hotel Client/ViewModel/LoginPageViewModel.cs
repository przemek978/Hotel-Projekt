using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.Services;
using Hotel_Client.View;
using Hotel_Client.Models.Util;

namespace Hotel_Client.ViewModel
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IAlertService _alertService;
        private readonly IShareService _shareService;

        private string _username = "User1";
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        private string _password = "Test123";
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public LoginPageViewModel(IHotelRepository hotelRepository, IAlertService alertService, IShareService shareService)
        {
            _hotelRepository = hotelRepository;
            _alertService = alertService;
            _shareService = shareService;
        }

        public async Task Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return;

            try
            {
                var id = await _hotelRepository.Login(Username, Password);

                if (id != 0)
                {
                    App.UserId = id;
                    App.Username = Username;
                    App.Password = Password;
                    //Application.Current.MainWindow.DataContext = new HomePageViewModel(_hotelRepository, _alertService, _shareService);
                    var homeWindow = new HomePage(_hotelRepository, _alertService, _shareService);
                    homeWindow.Show();
                    Application.Current.MainWindow.Close();
                }
            }
            catch (Exception e)
            {
                await _alertService.ShowAlertAsync(UIText.ErrorTitle, e.Message, UIText.CancelLabelButton);
            }
        }
    }
}