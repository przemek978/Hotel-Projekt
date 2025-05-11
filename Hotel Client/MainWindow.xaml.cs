using Hotel_Client.Models;
using Hotel_Client.OnlineReceptionImplService;
using Hotel_Client.Repositories;
using Hotel_Client.Repositories.Interfaces;
using System.Data;
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
        private readonly IHotelRepository _hotelRepository;

        public MainWindow()
        {
            InitializeComponent();
            _hotelRepository = new HotelRepository();
            Login();
        }

        public async void Login()
        {
            var client = new OnlineReceptionImplClient();
            try
            {
                //var result = await _hotelRepository.Login("User1", "Test123");
                //var userId = result;

                //var rooms = await _hotelRepository.GetRooms(DateTime.UtcNow, DateTime.UtcNow.AddDays(1));
                //var reservationId = await _hotelRepository.MakeReservation(
                //    new List<string> { "11", "12" }, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(3), "prosze o wifi", userId);

                //var reservationsFromDb = await _hotelRepository.GetReservations(userId);
                //var reservationToUpdate = reservationsFromDb.ToList()[0];

                //reservationToUpdate.Notes = "Edytowany opis";
                //await _hotelRepository.ModifyReservation(
                //   reservationToUpdate,
                //   userId
                //);


                //var reservationsAfterUpdate = await _hotelRepository.GetReservations(userId);
                //var reseravtionAfterUpdate = reservationsAfterUpdate.ToList()[0];

                //await _hotelRepository.GetConfirmationDocument(reservationId, userId);
                //await _hotelRepository.CancelReservation(reservationId, userId);

                //var reservationsAfterCancel = await _hotelRepository.GetReservations(userId);
                //var reseravtionAfterCancel = reservationsAfterUpdate.ToList()[0];

                Console.WriteLine("End");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}