using HotelReservation.OnlineReceptionImplService;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Login();
        }

        public async void Login()
        {
            var client = new OnlineReceptionImplClient();
            try
            {
                var result = await client.loginAsync("User1", "Test123");

                var userId = result.@return;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}