using System.Windows;
using System.Windows.Controls;
using Hotel_Client.ViewModel;

namespace Hotel_Client.View
{
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
            var viewModel = new LoginPageViewModel();
            DataContext = viewModel;

            // Powi�zanie has�a z ViewModelem, zak�adaj�c �e ViewModel ma Password jako string
            PasswordBox.PasswordChanged += (s, e) =>
            {
                viewModel.Password = PasswordBox.Password;
            };
        }
    }
}