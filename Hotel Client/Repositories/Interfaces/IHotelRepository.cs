using Hotel_Client.Models;
//using HotelReservation.Models.Requests;

namespace Hotel_Client.Repositories.Interfaces;

public interface IHotelRepository
{
    public Task<int> Login(string username, string password);
    public Task<IReadOnlyCollection<Room>?>? GetRooms(DateTime from, DateTime to);
    public Task<int> MakeReservation(List<string> roomNumbers, DateTime dateFrom, DateTime dateTo, string notes,
        int userId);
    public Task<IReadOnlyCollection<Reservation>?> GetReservations(int userId);
    public Task CancelReservation(int reservationId, int userId);
    public Task ModifyReservation(Reservation reservation, int userId);
    public Task<byte[]> GetConfirmationDocument(int reservationId, int userId);
}