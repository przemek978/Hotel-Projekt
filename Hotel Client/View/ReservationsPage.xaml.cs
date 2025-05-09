using System.Windows;

namespace Hotel_Client.View
{
    public partial class ReservationsPage : Window
    {
        public ReservationsPage()
        {
            InitializeComponent();
            DataContext = new ReservationsPageViewModel(); // Można podmienić na DI
        }
    }
}
