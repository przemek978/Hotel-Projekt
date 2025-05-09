using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Client.View
{
    public partial class RoomDetailsPage : Window
    {
        public RoomDetailsPage()
        {
            InitializeComponent();
            DataContext = new RoomDetailsPageViewModel(); // Można podmienić na DI
        }
    }
}
