using System.Windows;

namespace Hotel_Client.View
{
    public partial class ReservationDetailsPage : Window
    {
        public ReservationDetailsPage()
        {
            InitializeComponent();
            DataContext = new ReservationDetailsPageViewModel(); // Można podmienić na DI
        }
    }
}
