using System.Windows;

namespace Hotel_Client.View
{
    public partial class MakeReservationPage : Window
    {
        public MakeReservationPage()
        {
            InitializeComponent();
            DataContext = new MakeReservationPageViewModel(); // Można podmienić na DI
        }
    }
}
