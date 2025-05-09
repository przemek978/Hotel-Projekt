using Hotel_Client.Models;

namespace Hotel_Client.Services.Interfaces
{
    public interface IShareService
    {
        Task Add<T>(string key, T value) where T : class;
        Task AddRoom<T>(string key, Room value);
        Task<T?> GetValue<T>(string key) where T : class;
        Task<IReadOnlyCollection<Room>> GetBookedRooms();
        Task RemoveRoom<T>(Room value) where T : class;
    }
}