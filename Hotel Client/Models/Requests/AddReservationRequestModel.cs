using System.Text.Json.Serialization;

namespace HotelReservation.Models.Requests;

public class AddReservationRequestModel
{
    [JsonPropertyName("from")] public string From { get; set; } = string.Empty;

    [JsonPropertyName("to")] public string To { get; set; } = string.Empty;

    [JsonPropertyName("rooms")] public List<int> Rooms { get; set; } = [];

    [JsonPropertyName("ownersId")] public int OwnersId { get; set; }

    [JsonPropertyName("notes")] public string Notes { get; set; } = string.Empty;
}