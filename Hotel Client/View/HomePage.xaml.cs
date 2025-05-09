using System.Windows;

namespace Hotel_Client.View
{
    public partial class HomePage : Window
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = new HomePageViewModel(); // Można podmienić na DI
        }
    }
}
