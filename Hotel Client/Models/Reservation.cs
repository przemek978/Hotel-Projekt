using System.Text.Json.Serialization;
using Hotel_Client.Models.Util;

namespace Hotel_Client.Models;

public class Reservation
{
    public int Number { get; set; }
    
    [JsonConverter(typeof(CustomDateTimeConverter))] 
    public DateTime From { get; set; }
    
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime To { get; set; }
    public List<Room> Rooms { get; set; } = [];
    public int OwnersId { get; set; }
    public string Notes { get; set; } = string.Empty;
}