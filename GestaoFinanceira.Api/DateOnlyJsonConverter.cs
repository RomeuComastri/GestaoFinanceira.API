using Newtonsoft.Json;
using System;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var date = reader.Value.ToString();
        return DateOnly.Parse(date);  // Convertendo para DateOnly
    }

    public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString("yyyy-MM-dd"));  // Formato de data "yyyy-MM-dd"
    }
}
