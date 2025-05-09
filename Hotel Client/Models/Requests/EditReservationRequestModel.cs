using System.Text.Json.Serialization;

namespace Hotel_Client.Models.Requests;

public class EditReservationRequestModel
{
    [JsonPropertyName("number")]
    public int Number { get; set; }
    
    [JsonPropertyName("from")]
    public string From { get; set; } = string.Empty;
   
    [JsonPropertyName("to")]
    public string To { get; set; } = string.Empty;

    [JsonPropertyName("rooms")]
    //public List<Room> Rooms { get; set; } = [];
    public List<int> Rooms { get; set; } = [];
    [JsonPropertyName("ownersId")]
    public int OwnersId { get; set; }
    
    [JsonPropertyName("notes")]
    public string Notes { get; set; } = string.Empty;
}