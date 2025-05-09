using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Client.Models;
using Hotel_Client.Services.Interfaces;

namespace Hotel_Client.Services
{
    public class ShareService : IShareService
    {
        private Dictionary<string, object> RoomDictionary { get; set; } = new();
        private Dictionary<string, object> RoomsToBook { get; set; } = new();

        public Task Add<T>(string key, T value) where T : class
        {
            RoomDictionary[key] = value;
            return Task.CompletedTask;
        }

        public Task AddRoom<T>(string key, Room value)
        {
            RoomsToBook[key] = value;
            return Task.CompletedTask;
        }

        public Task<T?> GetValue<T>(string key) where T : class
        {
            return !RoomDictionary.TryGetValue(key, out var value)
                ? Task.FromResult<T?>(null)
                : Task.FromResult(value as T);
        }

        public Task<IReadOnlyCollection<Room>> GetBookedRooms()
        {
            var bookedRooms = RoomsToBook.Select(r => r.Value).Cast<Room>().ToList();
            return Task.FromResult<IReadOnlyCollection<Room>>(bookedRooms);
        }

        public Task RemoveRoom<T>(Room value) where T : class
        {
            var key = RoomsToBook.FirstOrDefault(kvp => kvp.Value == value).Key;
            RoomsToBook.Remove(key);
            return Task.CompletedTask;
        }
    }
}