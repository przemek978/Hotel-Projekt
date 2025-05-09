using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hotel_Client.Models.Util;

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString();

        if (dateString.EndsWith("[UTC]"))
        {
            dateString = dateString.Replace("[UTC]", "");
        }

        return DateTime.Parse(dateString, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ[UTC]"));
    }
}
