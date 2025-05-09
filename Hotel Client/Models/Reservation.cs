using System.Text.Json.Serialization;
using HotelReservation.Models.Util;

namespace HotelReservation.Models;

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