using System.Windows;
using System.Windows.Controls;
using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.ViewModel;

namespace Hotel_Client.View
{
    public partial class LoginPage : Window
    {
        public LoginPage(IHotelRepository hotelRepository, IAlertService alertService, IShareService shareService)
        {
            InitializeComponent();
            var viewModel = new LoginPageViewModel(hotelRepository, alertService, shareService);
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginPageViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.Password = passwordBox.Password;
            }
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginPageViewModel viewModel)
            {
                await viewModel.Login();
            }
        }
    }
}