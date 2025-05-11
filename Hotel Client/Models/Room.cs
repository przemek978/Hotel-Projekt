using System.Text.Json.Serialization;

namespace Hotel_Client.Models
{
    public class Room
    {
        [JsonPropertyName("roomNumber")] public string RoomNumber { get; set; } = string.Empty;
        [JsonPropertyName("floorNumber")] public int FloorNumber { get; set; }
        [JsonPropertyName("hasDoubleBed")] public bool HasDoubleBed { get; set; }

        [JsonPropertyName("numberOfSingleBeds")]
        public int NumberOfSingleBeds { get; set; }

        [JsonPropertyName("roomSize")] public double RoomSize { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; } = default!;
    }
}
