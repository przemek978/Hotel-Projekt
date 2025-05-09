using System.Threading.Tasks;
using System.Windows;
using Hotel_Client.Services.Interfaces;

namespace Hotel_Client.Services
{
    public class AlertService : IAlertService
    {
        public Task ShowAlertAsync(string title, string message, string cancel)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
            return Task.CompletedTask;
        }
    }
}