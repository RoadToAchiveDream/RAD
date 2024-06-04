using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RAD.Services.Helpers;

public class EnumToStringConverter : StringEnumConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
}
